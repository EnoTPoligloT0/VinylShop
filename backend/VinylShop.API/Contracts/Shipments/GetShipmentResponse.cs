namespace VinylShop.API.Contracts.Shipments;

public record GetShipmentResponse(
    Guid ShipmentId,
    Guid OrderId,
    DateTime ShipmentDate,
    string TrackingNumber,
    string ShipmentStatus
);