using System.ComponentModel.DataAnnotations;
using VinylShop.API.Contracts.OrderItems;

namespace VinylShop.API.Contracts.Orders;

public record CreateOrderRequest(
    [Required] Guid UserId,
    [Required] DateTime OrderDate,
    [Required] decimal TotalAmount
);