namespace VinylShop.Core.Interfaces.UnitOfWork;

public interface ITransaction : IDisposable
{
    Task CommitAsync();
    Task RollbackAsync();
}