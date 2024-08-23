using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Repositories;

public interface IShipmentRepository
{
    Task Create(Shipment shipment);
    Task Delete(Guid id);
    Task<List<Shipment>> Get();
    Task<Shipment> GetById(Guid id);
    Task<Shipment> GetByOrderId(Guid orderId);
    Task Update(Guid id, string trackingNumber, string shipmentStatus);
}