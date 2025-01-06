namespace VinylShop.Core.Interfaces.UnitOfWork;

public interface ITransactionManager
{
    Task ExecuteInTransactionAsync(Func<Task> action);
}