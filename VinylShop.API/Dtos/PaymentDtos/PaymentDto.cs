namespace VinylShop.API.Dtos.PaymentDtos;

public class PaymentDto
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}