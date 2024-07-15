using VinylShop.Core.Models;

namespace VinylShop.DataAccess.Entities;

public class PaymentEntity
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
}