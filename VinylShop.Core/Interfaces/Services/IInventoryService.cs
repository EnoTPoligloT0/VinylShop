using ErrorOr;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IInventoryService
{
    Task<ErrorOr<bool>> CheckStockAvailabilityAsync(Guid productId, int requestedQuantity);
    Task<ErrorOr<Success>> ReduceStockAsync(Guid productId, int quantity);
    Task<ErrorOr<Success>> RestoreStockAsync(Guid productId, int quantity);
}
