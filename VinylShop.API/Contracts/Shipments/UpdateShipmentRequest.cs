namespace VinylShop.API.Contracts.Shipments;

public class UpdateShipmentRequest
{
    public DateTime ShipmentDate { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string ShipmentStatus { get; set; } = string.Empty;
}