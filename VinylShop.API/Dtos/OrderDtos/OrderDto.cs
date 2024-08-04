using VinylShop.API.Dtos.OrderItemDtos;
using VinylShop.API.Dtos.PaymentDtos;
using VinylShop.API.Dtos.ShipmentDtos;
using VinylShop.API.Dtos.UserDtos;

namespace VinylShop.API.Dtos.OrderDtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserDto User { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = [];
    public List<PaymentDto> Payments { get; set; } = [];
    public List<ShipmentDto> Shipments { get; set; } = [];
}