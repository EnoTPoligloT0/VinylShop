using Microsoft.AspNetCore.Mvc;
using VinylShop.API.Contracts.OrderItems;
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
        OrderItemService orderItemService)
    {
        var orderItems = await orderItemService.GetOrderItems(orderId);

        var response = orderItems
            .Select(l => new GetOrderItemResponse(l.Id, l.OrderId, l.VinylId, l.Quantity, l.UnitPrice));

        return Results.Ok(response);
    }

    private static async Task<IResult> GetOrderItemById(
        [FromRoute] Guid id,
        OrderItemService orderItemService)
    {
        var orderItem = await orderItemService.GetOrderItemById(id);

        var response = new GetOrderItemResponse(id, orderItem.OrderId, orderItem.VinylId, orderItem.Quantity,
            orderItem.UnitPrice);

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