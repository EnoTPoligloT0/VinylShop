using Microsoft.EntityFrameworkCore.Storage;
using VinylShop.Core.Interfaces.UnitOfWork;
using VinylShop.DataAccess;

namespace VinylShop.Application;

public class TransactionManager : ITransactionManager
{
    private readonly VinylShopDbContext _context;
    private IDbContextTransaction _transaction;

    public TransactionManager(VinylShopDbContext context)
    {
        _context = context;
    }

    private async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return _transaction;
    }

    private async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    private async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public async Task ExecuteInTransactionAsync(Func<Task> action)
    {
        var transaction = await BeginTransactionAsync();

        try
        {
            await action();
            await CommitAsync();
        }
        catch (Exception)
        {
            await RollbackAsync();
            throw;
        }
    }
}
