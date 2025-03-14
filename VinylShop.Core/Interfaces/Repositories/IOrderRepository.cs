using VinylShop.Core.Enums;
using VinylShop.Core.Models;


namespace VinylShop.Core.Interfaces.Repositories;

public interface IOrderRepository
{
    Task Create(Order order);

    Task Delete(Guid id);

    Task<List<Order>> Get(int page, int pageSize);

    Task<Order> GetById(Guid id);

    Task Update(Guid id, DateTime orderDate, decimal totalAmount, List<OrderItem> orderItems);

    Task UpdateStatusAsync(Guid id, Status status);

    Task<bool> OrderExistsAsync(Guid orderId);

    Task<int> GetTotalOrderCount();
}