using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using VinylShop.API.Contracts.OrderItems;
using VinylShop.API.Contracts.Payments;
using VinylShop.API.Contracts.Shipments;
using VinylShop.Application.Services;
using VinylShop.Core.Models;
using Exception = System.Exception;

namespace VinylShop.API.Endpoints;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("payments");

        endpoints.MapPost("/{orderId:guid}", CreatePayment);
        endpoints.MapPost("/create-checkout-session", CreateCheckoutSession);
        endpoints.MapPost("/verify-payment/{sessionId}", VerifyPayment);
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
            request.PaymentMethod,
            request.StripePaymentId
        );

        if (!paymentResult.IsSuccess) return Results.BadRequest(paymentResult.Error);

        await paymentService.CreatePayment(paymentResult.Value);

        return Results.Ok(paymentResult.Value);
    }
    private static async Task<IResult> CreateCheckoutSession(
        [FromBody] TotalAmountRequest totalAmountRequest,
        [FromServices] PaymentService paymentService)
    {
        if (totalAmountRequest == null || totalAmountRequest.TotalAmount <= 0)
        {
            return Results.BadRequest("Total amount is required and must be greater than 0.");
        }

        try
        {
            var lineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Order Total",
                        },
                        UnitAmount = (long)(totalAmountRequest.TotalAmount * 100),
                    },
                    Quantity = 1,
                }
            };

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:3000/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:3000/cancel",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return Results.Json(new { sessionId = session.Id });
        }
        catch (Exception ex)
        {
            return Results.Problem(detail: ex.Message, statusCode: 500);
        }
    }

    private static async Task<IResult> VerifyPayment(
        [FromRoute] string sessionId,
        [FromServices] PaymentService paymentService)
    {
        Console.WriteLine($"VerifyPayment called with sessionId: {sessionId}");
        try
        {
            var service = new SessionService();
            var session = await service.GetAsync(sessionId);

            if (session.PaymentStatus != "paid")
            {
                return Results.BadRequest("Payment was not successful.");
            }

            if (string.IsNullOrWhiteSpace(session.ClientReferenceId))
            {
                return Results.BadRequest("Order ID is missing in the payment session.");
            }

            if (!Guid.TryParse(session.ClientReferenceId, out var orderId))
            {
                return Results.BadRequest("Invalid Order ID format.");
            }

            var amount = session.AmountTotal / 100m; // Convert cents to dollars

            var paymentResult = Payment.Create(
                Guid.NewGuid(),
                orderId,
                DateTime.UtcNow,
                (decimal)amount!,
                "card",
                sessionId
            );

            if (!paymentResult.IsSuccess)
            {
                return Results.BadRequest(paymentResult.Error);
            }

            await paymentService.CreatePayment(paymentResult.Value);

            return Results.Ok(new
            {
                success = true,
                message = "Payment verified and saved successfully.",
                orderId,
                amount,
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"VerifyPayment failed: {ex.Message}");
            return Results.Problem(detail: ex.Message, statusCode: 500);
        }
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