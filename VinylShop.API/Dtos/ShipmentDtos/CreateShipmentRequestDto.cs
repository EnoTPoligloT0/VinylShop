namespace VinylShop.API.Dtos.ShipmentDtos;

public class CreateShipmentRequestDto
{
    public Guid OrderId { get; set; }
    public DateTime ShipmentDate { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string ShipmentStatus { get; set; } = string.Empty;
}