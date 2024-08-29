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
        
        endpoints.MapGet("/{orderId:guid}", GetOrderById);
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
     private static async Task<IResult> GetOrderById(
            [FromRoute] Guid orderId,
            [FromServices] OrderService orderService,
            [FromServices] OrderItemService orderItemService,
            [FromServices] PaymentService paymentService,
            [FromServices] ShipmentService shipmentService,
            [FromServices] UserService userService,
            [FromServices] VinylService vinylService)
    {
        var order = await orderService.GetOrderById(orderId);

        if (order == null) return Results.NotFound();

        var orderItems = await orderItemService.GetOrderItemByOrderId(orderId);
        var payment = await paymentService.GetPaymentByOrderId(orderId);
        var shipment = await shipmentService.GetShipmentByOrderId(orderId);
        var user = await userService.GetUserById(order.UserId);

        var orderItemResponses = new List<GetOrderItemResponse>();

        foreach (var orderItem in orderItems)
        {
            var vinyl = await vinylService.GetVinylById(orderItem.VinylId);

            orderItemResponses.Add(new GetOrderItemResponse(
                orderItem.Id,
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
                    vinyl.IsAvailable)
            ));
        }

        var response = new GetOrderResponse(
            order.Id,
            order.UserId,
            new GetUserResponse(user.UserId, user.Email, user.PasswordHash),
            order.OrderDate,
            order.TotalAmount,
            orderItemResponses,
            new GetPaymentResponse(
                payment.PaymentId,
                payment.OrderId,
                payment.PaymentDate,
                payment.Amount,
                payment.PaymentMethod
            ),
            new GetShipmentResponse(
                shipment.ShipmentId,
                shipment.OrderId,
                shipment.ShipmentDate,
                shipment.TrackingNumber,
                shipment.ShipmentStatus
            )
        );

        return Results.Ok(response);
    }
     
}