using System.ComponentModel.DataAnnotations;

namespace VinylShop.API.Contracts.Payments;

public record CreatePaymentRequest(
    [Required] Guid OrderId,
    [Required] DateTime PaymentDate,
    [Required] decimal Amount,
    [Required] string PaymentMethod
);