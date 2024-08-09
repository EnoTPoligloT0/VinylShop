using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Models;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly VinylShopDbContext _context;
    private readonly IMapper _mapper;

    public OrderItemRepository(VinylShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Create(OrderItem orderItem)
    {
        var orderItemEntity = new OrderItemEntity
        {
            Id = orderItem.Id,
            OrderId = orderItem.OrderId,
            VinylId = orderItem.VinylId,
            Quantity = orderItem.Quantity,
            UnitPrice = orderItem.UnitPrice,
        };

        await _context.OrderItems.AddAsync(orderItemEntity);
        await _context.SaveChangesAsync();
    }


    public async Task<List<OrderItem>> Get()
    {
        var orderItemEntities = await _context.OrderItems
            .AsNoTracking()
            .Include(o => o.Vinyl)
            .Include(o => o.Order)
            .ToListAsync();

        return _mapper.Map<List<OrderItem>>(orderItemEntities);
    }

    public async Task<OrderItem> GetById(Guid id)
    {
        var orderItemEntity = await _context.OrderItems
            .AsNoTracking()
            .Include(o => o.Vinyl)
            .Include(o => o.Order)
            .SingleOrDefaultAsync(c => c.Id == id) ?? throw new Exception();

        return _mapper.Map<OrderItem>(orderItemEntity);
    }

    public async Task Update(Guid id, int quantity)
    {
        await _context.OrderItems
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(
                s => s
                    .SetProperty(c => c.Quantity, quantity)
            );
    }

    public async Task Delete(Guid id)
    {
        await _context.OrderItems
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }
}
