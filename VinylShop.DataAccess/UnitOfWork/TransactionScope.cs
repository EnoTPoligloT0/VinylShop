using System.Data.Common;
using ErrorOr;
using Microsoft.EntityFrameworkCore.Storage;
using VinylShop.Core.Interfaces.UnitOfWork;

namespace VinylShop.Application;

public class TransactionScope : ITransaction
{
    private readonly IDbContextTransaction _transaction;

    public TransactionScope(IDbContextTransaction transaction)
    {
        _transaction = transaction;
    }

    public async Task CommitAsync() => await _transaction.CommitAsync();
    public async Task RollbackAsync() => await _transaction.RollbackAsync();
    
    public void Dispose()
    {
       
    }
    
}