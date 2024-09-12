namespace VinylShop.API.Contracts.Payments;

public record GetPaymentResponse(
    Guid PaymentId,
    Guid OrderId,
    DateTime PaymentDate,
    decimal Amount,
    string PaymentMethod
);