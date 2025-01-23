using VinylShop.Core.Interfaces.Repositories;

namespace VinylShop.Core.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    IOrderItemRepository OrderItems { get; }
    IOrderRepository Orders { get; }
    IPaymentRepository Payments { get; }
    IShipmentRepository Shipments { get; }
    IUserRepository Users { get; }
    IVinylRepository Vinyls { get; }

    Task<bool> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}