namespace VinylShop.API.Dtos.OrderItemDto;

public class UpdateOrderItemRequestDto
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}