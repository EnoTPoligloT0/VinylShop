using VinylShop.Core.Enums;

namespace VinylShop.DataAccess.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get;  set;}

    public UserEntity User { get;  set;}
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get;  set;}

    public Status Status { get; set; }

    public List<OrderItemEntity> OrderItems { get; set; }
    public List<ShipmentEntity> Shipments { get; set; }
    public List<PaymentEntity> Payments { get; set; }
}