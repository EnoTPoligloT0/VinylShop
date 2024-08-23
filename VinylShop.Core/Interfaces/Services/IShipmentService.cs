using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IShipmentService
{
    Task CreateShipment(Shipment shipment);
    Task DeleteShipment(Guid id);
    Task<List<Shipment>> GetShipments();
    Task<Shipment> GetShipmentById(Guid id);
    Task<Shipment> GetShipmentByOrderId(Guid id);
    Task UpdateShipment(Guid id, string trackingNumber, string shipmentStatus);
}