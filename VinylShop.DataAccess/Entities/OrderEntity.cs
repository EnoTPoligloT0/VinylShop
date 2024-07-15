using VinylShop.Core.Models;

namespace VinylShop.DataAccess.Entities;

public class OrderEntity
{
    public Guid Id { get; }
    public Guid UserId { get; }

    public User User { get; }
    public DateTime OrderDate { get; }
    public decimal TotalAmount { get; }

    public List<OrderItem> OrderItems { get; }
}