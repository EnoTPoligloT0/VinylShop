using VinylShop.Core.Dtos.OrderItemDtos;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IOrderService
{
    Task CreateOrderr(Order order);

    Task DeleteOrder(Guid id);

    Task<List<Order>> GetOrders();

    Task<Order> GetOrderById(Guid id);

    Task UpdateOrder(Guid id, DateTime orderDate, decimal totalAmount, List<UpdateOrderItemRequestDto> orderItems);
}