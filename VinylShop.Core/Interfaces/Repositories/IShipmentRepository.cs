using VinylShop.Core.Dtos.ShipmentDtos;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Repositories;

public interface IShipmentRepository
{
    Task Create(Shipment shipment);
    Task Delete(Guid id);
    Task<List<Shipment>> Get();
    Task<Shipment> GetById(Guid id);
    Task Update(Guid id, UpdateShipmentRequestDto updateShipmentRequestDto);
}