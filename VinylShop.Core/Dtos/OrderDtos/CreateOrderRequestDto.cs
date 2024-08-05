using VinylShop.Core.Dtos.OrderItemDtos;

namespace VinylShop.Core.Dtos.OrderDtos;

public class CreateOrderRequestDto
{
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<CreateOrderItemRequestDto> OrderItems { get; set; } = new List<CreateOrderItemRequestDto>();
}