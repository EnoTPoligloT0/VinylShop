using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Interfaces.UnitOfWork;
using VinylShop.Core.Models;

namespace VinylShop.Application.Services;

public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository _shipmentRepository;
    private readonly ITransactionManager _transactionManager;

    public ShipmentService(IShipmentRepository shipmentRepository, ITransactionManager transactionManager)
    {
        _shipmentRepository = shipmentRepository;
        _transactionManager = transactionManager;
    }

    public async Task CreateShipment(Shipment shipment)
    {
        await _transactionManager.ExecuteInTransactionAsync(async () =>
        {
            await _shipmentRepository.Create(shipment);
        });
    }


    public async Task<List<Shipment>> GetShipments()
    {
        return await _shipmentRepository.Get();
    }

    public async Task<Shipment> GetShipmentById(Guid id)
    {
        return await _shipmentRepository.GetById(id);
    }

    public async Task<Shipment> GetShipmentByOrderId(Guid orderId)
    {
        return await _shipmentRepository.GetByOrderId(orderId);
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