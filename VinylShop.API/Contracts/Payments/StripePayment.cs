namespace VinylShop.API.Contracts.Payments;


public record StripePayment(
    Guid PaymentId,
    Guid OrderId,
    decimal Amount,
    string Currency = "usd",
    string PaymentMethodId = ""
);