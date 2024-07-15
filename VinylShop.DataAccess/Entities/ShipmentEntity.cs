using VinylShop.Core.Models;

namespace VinylShop.DataAccess.Entities;

public class ShipmentEntity
{
    public Guid ShipmentId { get; set; }
    public Guid OrderId { get; set; }
    public DateTime ShipmentDate { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string ShipmentStatus { get; set; } = string.Empty;
    public Order Order { get; set; }
}