using VinylShop.Core.Enums;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IOrderService
{
    Task CreateOrder(Order order);

    Task DeleteOrder(Guid id);

    Task<List<Order>> GetOrders(int page, int pageSize);

    Task<Order> GetOrderById(Guid id);

    Task UpdateOrder(Guid id, DateTime orderDate, decimal totalAmount, List<OrderItem> orderItems);

    Task<int> GetTotalOrderCount();

    Task UpdateStatus(Guid id, Status status);
}