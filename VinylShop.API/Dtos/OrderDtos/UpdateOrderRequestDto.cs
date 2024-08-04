using VinylShop.API.Dtos.OrderItemDtos;

namespace VinylShop.API.Dtos.OrderDtos;

public class UpdateOrderRequestDto
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<UpdateOrderItemRequestDto> OrderItems { get; set; } = new List<UpdateOrderItemRequestDto>();
}