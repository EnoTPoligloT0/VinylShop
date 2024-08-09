using VinylShop.API.Contracts.OrderItems;

namespace VinylShop.API.Contracts.Orders;

public class UpdateOrderRequest
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<UpdateOrderItemRequest> OrderItems { get; set; } = new List<UpdateOrderItemRequest>();
}