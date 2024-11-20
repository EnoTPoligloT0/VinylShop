using System.ComponentModel.DataAnnotations;

namespace VinylShop.API.Contracts.Payments;

public class TotalAmountRequest
{
    [Required]
    public decimal TotalAmount { get; set; }
}
