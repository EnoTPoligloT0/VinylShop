using VinylShop.Core.Enums;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IOrderService
{
    Task CreateOrder(Order order);

    Task DeleteOrder(Guid id);

    Task<List<Order>> GetOrders();

    Task<Order> GetOrderById(Guid id);

    Task UpdateOrder(Guid id, DateTime orderDate, decimal totalAmount, List<OrderItem> orderItems);

    Task UpdateStatus(Guid id, Status status);
}