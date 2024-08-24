using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task Create(OrderItem orderItem);
    Task Delete(Guid id);
    Task<List<OrderItem>> Get(Guid orderId);
    Task<OrderItem> GetById(Guid id);
    Task<OrderItem> GetByOrderId(Guid orderId);
    Task Update(Guid id, int quantity);
}