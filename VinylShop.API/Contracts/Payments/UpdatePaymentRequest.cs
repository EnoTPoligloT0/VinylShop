namespace VinylShop.API.Contracts.Payments;

public record UpdatePaymentRequest(
    DateTime PaymentDate,
    decimal Amount,
    string PaymentMethod
);