using VinylShop.Core.Models;

namespace VinylShop.DataAccess.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get;  set;}

    public UserEntity User { get;  set;}
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get;  set;}

    public List<OrderItemEntity> OrderItems { get; set; }
}