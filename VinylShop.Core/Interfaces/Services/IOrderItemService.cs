using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IOrderItemService
{
    Task CreateOrderItem(OrderItem orderItem);
    Task DeleteOrderItem(Guid id);
    Task<List<OrderItem>> GetOrderItems(Guid orderId);
    Task<OrderItem> GetOrderItemById(Guid id);
    Task UpdateOrderItem(Guid id, int quantity);
}