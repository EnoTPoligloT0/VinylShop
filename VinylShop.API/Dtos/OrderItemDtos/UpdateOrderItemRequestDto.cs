namespace VinylShop.API.Dtos.OrderItemDtos;

public class UpdateOrderItemRequestDto
{
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}