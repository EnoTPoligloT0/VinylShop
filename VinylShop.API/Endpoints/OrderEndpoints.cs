using Microsoft.AspNetCore.Mvc;
using VinylShop.API.Contracts.OrderItems;
using VinylShop.API.Contracts.Orders;
using VinylShop.API.Contracts.Payments;
using VinylShop.API.Contracts.Shipments;
using VinylShop.API.Contracts.Users;
using VinylShop.API.Contracts.Vinyls;
using VinylShop.Application.Services;
using VinylShop.Core.Models;

namespace VinylShop.API.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("orders");

        endpoints.MapPost("/", CreateOrder);
        // endpoints.MapGet("/{orderId:guid}", GetOrderById);
        // endpoints.MapGet("/", GetAllOrders);
        // endpoints.MapPut("/{orderId:guid}", UpdateOrder);
        // endpoints.MapDelete("/{orderId:guid}", DeleteOrder);

        return endpoints;
    }

    private static async Task<IResult> CreateOrder(
        [FromBody] CreateOrderRequest request,
        HttpContext context,
        OrderService orderServices)
    {
        var orderResult = Order.Create(
            Guid.NewGuid(),
            request.UserId,
            request.OrderDate,
            request.TotalAmount
        );
        if (!orderResult.IsSuccess) return Results.BadRequest(orderResult.Error);

        var order = orderResult.Value;

        await orderServices.CreateOrder(order);

        return Results.Ok(order);

    }
}