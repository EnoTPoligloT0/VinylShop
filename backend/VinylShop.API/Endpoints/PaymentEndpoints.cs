using Microsoft.AspNetCore.Mvc;
using VinylShop.API.Contracts.Payments;
using VinylShop.API.Contracts.Shipments;
using VinylShop.Application.Services;
using VinylShop.Core.Models;

namespace VinylShop.API.Endpoints;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("payments");

        endpoints.MapPost("/{orderId:guid}", CreatePayment);
        endpoints.MapGet("/{id:guid}", GetPaymentById);
        endpoints.MapGet("/order/{orderId:guid}", GetPaymentByOrderId);
        endpoints.MapPut("/{id:guid}", UpdatePayment);
        endpoints.MapDelete("/{id:guid}", DeletePayment);

        return endpoints;
    }

    private static async Task<IResult> CreatePayment(
        [FromRoute] Guid orderId,
        [FromBody] CreatePaymentRequest request,
        [FromServices] PaymentService paymentService)
    {
        var paymentResult = Payment.Create(
            Guid.NewGuid(),
            orderId,
            request.PaymentDate,
            request.Amount,
            request.PaymentMethod
            
        );

        if (!paymentResult.IsSuccess) return Results.BadRequest(paymentResult.Error);

        await paymentService.CreatePayment(paymentResult.Value);

        return Results.Ok(paymentResult.Value);
    }


    private static async Task<IResult> GetPaymentById(
        [FromRoute] Guid id,
        [FromServices] PaymentService paymentService)
    {
        var payment = await paymentService.GetPaymentById(id);

        var response = new GetPaymentResponse
        (
            id,
            payment.OrderId,
            payment.PaymentDate,
            payment.Amount,
            payment.PaymentMethod
        );

        return Results.Ok(response);
    }

    private static async Task<IResult> GetPaymentByOrderId(
        [FromRoute] Guid orderId,
        [FromServices] PaymentService paymentService)
    {
        var payment = await paymentService.GetPaymentByOrderId(orderId);

        var response = new GetPaymentResponse
        (
            payment.PaymentId,
            orderId,
            payment.PaymentDate,
            payment.Amount,
            payment.PaymentMethod
        );

        return Results.Ok(response);
    }

    private static async Task<IResult> UpdatePayment(
        [FromRoute] Guid id,
        UpdatePaymentRequest request,
        [FromServices] PaymentService paymentService)
    {
        await paymentService.UpdatePayment(id, request.Amount, request.PaymentMethod);

        return Results.Ok();
    }

    private static async Task<IResult> DeletePayment(
        [FromRoute] Guid id,
        [FromServices] PaymentService paymentService)
    {
        await paymentService.DeletePayment(id);

        return Results.Ok();
    }
}