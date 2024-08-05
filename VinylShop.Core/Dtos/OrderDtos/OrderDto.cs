using VinylShop.Core.Dtos.OrderItemDtos;
using VinylShop.Core.Dtos.PaymentDtos;
using VinylShop.Core.Dtos.ShipmentDtos;
using VinylShop.Core.Dtos.UserDtos;

namespace VinylShop.Core.Dtos.OrderDtos;

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