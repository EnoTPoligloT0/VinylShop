using System.ComponentModel.DataAnnotations;

namespace VinylShop.API.Contracts.OrderItems;

public record CreateOrderItemRequest(
    [Required] int Quantity,
    [Required] decimal UnitPrice
);