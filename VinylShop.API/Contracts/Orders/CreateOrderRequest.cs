using VinylShop.API.Contracts.OrderItems;

namespace VinylShop.API.Contracts.Orders;

public class CreateOrderRequest
{
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CreateOrderItemRequest> OrderItems { get; set; } = new List<CreateOrderItemRequest>();
}