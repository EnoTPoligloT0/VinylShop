using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Models;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly VinylShopDbContext _context;
    private readonly IMapper _mapper;

    public ShipmentRepository(VinylShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Create(Shipment shipment)
    {
        var shipmentEntity = new ShipmentEntity
        {
            ShipmentId = shipment.ShipmentId,
            OrderId = shipment.OrderId,
            ShipmentDate = shipment.ShipmentDate,
            TrackingNumber = shipment.TrackingNumber,
            ShipmentStatus = shipment.ShipmentStatus,
        };

        await _context.Shipment.AddAsync(shipmentEntity);
        await _context.SaveChangesAsync();
    }


    public async Task<List<Shipment>> Get()
    {
        var shipmentEntities = await _context.Shipment
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<List<Shipment>>(shipmentEntities);
    }

    public async Task<Shipment> GetById(Guid id)
    {
        var shipmentEntity = await _context.Shipment
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.ShipmentId == id);

        return _mapper.Map<Shipment>(shipmentEntity);
    }

    public async Task<Shipment> GetByOrderId(Guid orderId)
    {
        var shipmentEntity = await _context.Shipment
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.OrderId == orderId);

        return _mapper.Map<Shipment>(shipmentEntity);
    }

    public async Task Update(Guid id, string trackingNumber, string shipmentStatus)
    {
        await _context.Shipment
            .Where(c => c.ShipmentId == id)
            .ExecuteUpdateAsync(
                s => s
                    .SetProperty(c => c.TrackingNumber, trackingNumber)
                    .SetProperty(c => c.ShipmentStatus, shipmentStatus)
            );
        
        
    }

    public async Task Delete(Guid id)
    { 
        await _context.Shipment
            .Where(c => c.ShipmentId == id)
            .ExecuteDeleteAsync();
    }
}