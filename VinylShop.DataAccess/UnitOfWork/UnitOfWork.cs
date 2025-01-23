using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.UnitOfWork;
using VinylShop.DataAccess;

namespace VinylShop.Application;

public class UnitOfWork : IUnitOfWork
{
    private readonly VinylShopDbContext _context;

    public UnitOfWork(VinylShopDbContext context)
    {
        _context = context;
    }

    public IOrderItemRepository OrderItems { get; }
    public IOrderRepository Orders { get; }
    public IPaymentRepository Payments { get; }
    public IShipmentRepository Shipments { get; }
    public IUserRepository Users { get; }
    public IVinylRepository Vinyls { get; }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task BeginTransactionAsync()
    {
         await _context.Database.BeginTransactionAsync();
    }
    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }
    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }
}