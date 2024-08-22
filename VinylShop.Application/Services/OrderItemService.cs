using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;

namespace VinylShop.Application.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemsRepository;

    public OrderItemService(IOrderItemRepository orderItemsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
    }

    public async Task CreateOrderItem(OrderItem orderItem)
    {
        await _orderItemsRepository.Create(orderItem);
    }


    public async Task<List<OrderItem>> GetOrderItems(Guid orderId)
    {
        return await _orderItemsRepository.Get(orderId);
    }

    public async Task<OrderItem> GetOrderItemById(Guid id)
    {
        return await _orderItemsRepository.GetById(id);
    }

    public async Task UpdateOrderItem(Guid id, int quantity)
    {
         await _orderItemsRepository.Update(id, quantity);
    }
    
    public async Task DeleteOrderItem(Guid id)
    {
        await _orderItemsRepository.Delete(id);
    }
}
