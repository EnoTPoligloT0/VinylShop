using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VinylShop.Core.Enums;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Models;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly VinylShopDbContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(VinylShopDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Create(Order order)
    {
        var orderEntity = new OrderEntity
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
        };
        await _context.Orders.AddAsync(orderEntity);
        await _context.SaveChangesAsync();
    }


    public async Task<List<Order>> Get()
    {
        var orderEntities = await _context.Orders
            .AsNoTracking()
            .Include(c=> c.User)
            .Include(c => c.OrderItems)
            .ToListAsync();

        return _mapper.Map<List<Order>>(orderEntities);
    }

    public async Task<Order> GetById(Guid id)
    {
        var orderEntity = await _context.Orders
            .AsNoTracking()
            .Include(c=> c.User)
            .Include(c => c.OrderItems)
            .SingleOrDefaultAsync(c => c.Id == id) ?? throw new Exception();
            

        return _mapper.Map<Order>(orderEntity);
    }

    public async Task Update(Guid id, DateTime orderDate, decimal totalAmount,
        List<OrderItem> orderItems)
    {
        await _context.Orders
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(
                s => s
                    .SetProperty(c => c.OrderDate, orderDate)
                    .SetProperty(c => c.TotalAmount, totalAmount)
            );
    }

    public async Task UpdateStatusAsync(Guid id, Status status)
    {
        await _context.Orders
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(u =>
                u.SetProperty(s => s.Status, status));
    }

    public async Task<bool> OrderExistsAsync(Guid orderId)
    {
        return await _context.Orders
            .AnyAsync(o => o.Id == orderId);
    }

    public async Task Delete(Guid id)
    {
        await _context.Orders
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
    }
}