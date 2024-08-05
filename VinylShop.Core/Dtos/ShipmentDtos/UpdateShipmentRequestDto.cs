namespace VinylShop.Core.Dtos.ShipmentDtos;

public class UpdateShipmentRequestDto
{
    public DateTime ShipmentDate { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string ShipmentStatus { get; set; } = string.Empty;
}