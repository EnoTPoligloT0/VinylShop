namespace VinylShop.API.Contracts.Shipments;

public record UpdateShipmentRequest(
    DateTime ShipmentDate,
    string TrackingNumber,
    string ShipmentStatus
);