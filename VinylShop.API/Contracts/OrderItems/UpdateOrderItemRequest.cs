namespace VinylShop.API.Contracts.OrderItems;

public class UpdateOrderItemRequest
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}