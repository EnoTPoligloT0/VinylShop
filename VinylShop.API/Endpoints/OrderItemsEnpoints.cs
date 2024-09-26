using Microsoft.AspNetCore.Mvc;
using VinylShop.API.Contracts.OrderItems;
using VinylShop.API.Contracts.Vinyls;
using VinylShop.Application.Services;
using VinylShop.Core.Models;

namespace VinylShop.API.Endpoints;

public static class OrderItemsEnpoints
{
    public static IEndpointRouteBuilder MapOrderItemsEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoits = app.MapGroup("orderItems");

        endpoits.MapPost("orderItem/{orderId:guid}", CreateOrderItem);

        endpoits.MapGet("orderItem/order/{orderId:guid}", GetOrderItemByOrderId);

        endpoits.MapGet("orderItem/{id:guid}", GetOrderItemById);

        endpoits.MapPut("orderItem/{id:guid}", UpdateOrderItem);

        endpoits.MapDelete("orderItem/{id:guid}", DeleteOrderItem);

        return endpoits;
    }

    private static async Task<IResult> CreateOrderItem(
        [FromRoute] Guid orderId,
        [FromBody] CreateOrderItemRequest request,
        HttpContext context,
        OrderItemService orderItemsServices)
    {
        var orderItemResult = OrderItem.Create(
            Guid.NewGuid(),
            orderId,
            request.VinylId,
            request.Quantity,
            request.UnitPrice
        );
        if (orderItemResult.IsSuccess)
        {
            var orderItem = orderItemResult.Value;

            await orderItemsServices.CreateOrderItem(orderItem);

            return Results.Ok(orderItem);
        }

        return Results.BadRequest(orderItemResult.Error);
    }

    private static async Task<IResult> GetOrderItemByOrderId(
        [FromRoute] Guid orderId,
        OrderItemService orderItemService,
        VinylService vinylService)
    {
        var orderItems = await orderItemService.GetOrderItemByOrderId(orderId);

        var responses = new List<GetOrderItemResponse>();
        
        foreach (var orderItem in orderItems)
        {
            var vinyl = await vinylService.GetVinylById(orderItem.VinylId);

            var response = new GetOrderItemResponse(
                orderItem.Id,
                orderId,
                orderItem.VinylId,
                orderItem.Quantity,
                orderItem.UnitPrice,
                new GetVinylResponse(
                    vinyl.Id,
                    vinyl.Title,
                    vinyl.Artist,
                    vinyl.Genre,
                    vinyl.ReleaseYear,
                    vinyl.Price,
                    vinyl.Stock,
                    vinyl.Description,
                    vinyl.IsAvailable,
                    Convert.ToBase64String(vinyl.Image))
            );
            responses.Add(response);
        }
        
        return Results.Ok(responses);
    }

    private static async Task<IResult> GetOrderItemById(
        [FromRoute] Guid id,
        OrderItemService orderItemService,
        VinylService vinylService)
    {
        var orderItem = await orderItemService.GetOrderItemById(id);
        var vinyl = await vinylService.GetVinylById(orderItem.VinylId);

        var response = new GetOrderItemResponse(
            id,
            orderItem.OrderId,
            orderItem.VinylId,
            orderItem.Quantity,
            orderItem.UnitPrice,
            new GetVinylResponse(
                vinyl.Id,
                vinyl.Title,
                vinyl.Artist,
                vinyl.Genre,
                vinyl.ReleaseYear,
                vinyl.Price,
                vinyl.Stock,
                vinyl.Description,
                vinyl.IsAvailable,
                Convert.ToBase64String(vinyl.Image))
        );

        return Results.Ok(response);
    }

    private static async Task<IResult> UpdateOrderItem(
        [FromRoute] Guid id,
        [FromBody] UpdateOrderItemRequest request,
        OrderItemService orderItemService)
    {
        await orderItemService.UpdateOrderItem(id, request.Quantity);

        return Results.Ok();
    }

    private static async Task<IResult> DeleteOrderItem(
        [FromRoute] Guid id,
        OrderItemService orderItemService)
    {
        await orderItemService.DeleteOrderItem(id);

        return Results.Ok();
    }
}