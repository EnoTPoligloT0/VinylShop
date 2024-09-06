using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IOrderItemService
{
    Task CreateOrderItem(OrderItem orderItem);
    Task DeleteOrderItem(Guid id);
    //todo Refactor name of GetOrderItem
    Task<List<OrderItem>> GetOrderItem(Guid orderId);
    Task<OrderItem> GetOrderItemById(Guid id);
    Task<List<OrderItem>> GetOrderItemByOrderId(Guid orderId);
    Task UpdateOrderItem(Guid id, int quantity);
}