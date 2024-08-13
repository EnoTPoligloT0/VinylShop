using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;

namespace VinylShop.Application.Services;

public class ShipmentService : IShipmentService
{
    public readonly IShipmentRepository _shipmentRepository;

    public ShipmentService(IShipmentRepository shipmentRepository)
    {
        _shipmentRepository = shipmentRepository;
    }

    public async Task CreateShipment(Shipment shipment)
    {
        await _shipmentRepository.Create(shipment);
    }


    public async Task<List<Shipment>> GetShipments()
    {
        return await _shipmentRepository.Get();
    }

    public async Task<Shipment> GetShipmentById(Guid id)
    {
        return await _shipmentRepository.GetById(id);
    }

    public async Task UpdateShipment(Guid id, string trackingNumber, string shipmentStatus)
    {
        await _shipmentRepository.Update(id, trackingNumber, shipmentStatus);
    }
    
    public async Task DeleteShipment(Guid id)
    {
        await _shipmentRepository.Delete(id);
    }
}
