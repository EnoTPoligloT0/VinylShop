namespace VinylShop.API.Contracts.OrderItems;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid VinylId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}