using VinylShop.Core.Dtos.OrderItemDtos;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task Create(OrderItem orderItem);
    Task Delete(Guid id);
    Task<List<OrderItem>> Get();
    Task<OrderItem> GetById(Guid id);
    Task Update(Guid id, int quantity);
}