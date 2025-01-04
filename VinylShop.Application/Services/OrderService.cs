using VinylShop.Core.Enums;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;

namespace VinylShop.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task CreateOrder(Order order)
    {
        await _orderRepository.Create(order);
    }


    public async Task<List<Order>> GetOrders()
    {
        return await _orderRepository.Get();
    }

    public async Task<Order> GetOrderById(Guid id)
    {
        return await _orderRepository.GetById(id);
    }

    public async Task<bool> OrderExistsAsync(Guid id)
    {
        return await _orderRepository.OrderExistsAsync(id);
    }

    public async Task UpdateOrder(Guid id, DateTime orderDate, decimal totalAmount, List<OrderItem> orderItems)
    {
        await _orderRepository.Update(id, orderDate, totalAmount, orderItems);
    }

    public async Task UpdateStatus(Guid id, Status status)
    {
        await _orderRepository.UpdateStatusAsync(id, status);
    }
    public async Task DeleteOrder(Guid id)
    {
        await _orderRepository.Delete(id);
    }
}
