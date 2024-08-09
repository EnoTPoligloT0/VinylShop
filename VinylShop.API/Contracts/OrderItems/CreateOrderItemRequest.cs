namespace VinylShop.API.Contracts.OrderItems;

public class CreateOrderItemRequest
{
    public Guid VinylId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}