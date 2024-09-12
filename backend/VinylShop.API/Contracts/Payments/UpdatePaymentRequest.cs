using System.ComponentModel.DataAnnotations;

namespace VinylShop.API.Contracts.Payments;

public record UpdatePaymentRequest(
    [Required] DateTime PaymentDate,
    [Required] decimal Amount,
    [Required] string PaymentMethod
);