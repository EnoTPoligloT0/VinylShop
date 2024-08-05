namespace VinylShop.Core.Dtos.PaymentDtos;

public class CreatePaymentRequestDto
{
    public Guid OrderId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}