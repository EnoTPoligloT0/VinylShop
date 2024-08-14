using System.ComponentModel.DataAnnotations;

namespace VinylShop.API.Contracts.Shipments;

public record CreateShipmentRequest(
    [Required] DateTime ShipmentDate,
    [Required] string TrackingNumber,
    [Required] string ShipmentStatus
);