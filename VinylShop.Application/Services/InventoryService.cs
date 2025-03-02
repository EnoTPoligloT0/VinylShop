using ErrorOr;
using VinylShop.Core.Errors;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Interfaces.UnitOfWork;
using VinylShop.Core.Models;

namespace VinylShop.Application.Services;

public class InventoryService : IInventoryService
{
    private readonly IVinylRepository _vinylRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InventoryService(IVinylRepository vinylRepository, IUnitOfWork unitOfWork)
    {
        _vinylRepository = vinylRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> CheckStockAvailabilityAsync(Guid productId, int requestedQuantity)
    {
        var product = await _vinylRepository.GetById(productId);
        return product == null
            ? Errors.Vinyl.NotFound
            : product.Stock >= requestedQuantity;
    }

    public async Task<ErrorOr<Success>> ReduceStockAsync(Guid productId, int quantity)
    {
        var product = await _vinylRepository.GetById(productId);
        return product switch
        {
            null => Errors.Vinyl.NotFound,
            _ when product.Stock < quantity => Errors.Vinyl.InsufficientStock,
            _ => await PerformStockReduction(product, quantity)
        };
    }
    
    public async Task<ErrorOr<Success>> RestoreStockAsync(Guid productId, int quantity)
    {
        var product = await _vinylRepository.GetById(productId);
        return product switch
        {
            null => Errors.Vinyl.NotFound,
            _ when product.Stock < quantity => Errors.Vinyl.InsufficientStock,
            _ => await PerformStockRestore(product, quantity)
        };
    }
    
    private async Task<ErrorOr<Success>> PerformStockReduction(Vinyl product, int quantity)
    {
        product.Stock -= quantity;
        await _vinylRepository.UpdateStock(product);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success;
    }

    private async Task<ErrorOr<Success>> PerformStockRestore(Vinyl product, int quantity)
    {
        product.Stock += quantity;
        await _vinylRepository.UpdateStock(product);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}