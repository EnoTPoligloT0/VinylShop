using System.ComponentModel.DataAnnotations;

namespace VinylShop.API.Contracts.OrderItems;

public record UpdateOrderItemRequest(
    [Required] int Quantity,
    [Required] decimal UnitPrice
);