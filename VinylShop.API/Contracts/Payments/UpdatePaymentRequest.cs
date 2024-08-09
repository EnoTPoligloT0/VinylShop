namespace VinylShop.API.Contracts.Payments;

public class UpdatePaymentRequest
{
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}