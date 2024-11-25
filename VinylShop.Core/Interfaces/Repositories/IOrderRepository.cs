using VinylShop.Core.Models;


namespace VinylShop.Core.Interfaces.Repositories;

public interface IOrderRepository
{
    Task Create(Order order);

    Task Delete(Guid id);

    Task<List<Order>> Get();

    Task<Order> GetById(Guid id);

    Task Update(Guid id, DateTime orderDate, decimal totalAmount, List<OrderItem> orderItems);

    Task<bool> OrderExistsAsync(Guid orderId);
}