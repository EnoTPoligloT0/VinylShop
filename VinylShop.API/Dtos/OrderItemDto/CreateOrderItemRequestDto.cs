namespace VinylShop.API.Dtos.OrderItemDto;

public class CreateOrderItemRequestDto
{
    public Guid VinylId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}