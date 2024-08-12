using System.ComponentModel.DataAnnotations;
using VinylShop.API.Contracts.OrderItems;

namespace VinylShop.API.Contracts.Orders;

public record UpdateOrderRequest
(
     [Required] DateTime OrderDate,
     [Required] decimal TotalAmount,
     [Required] List<UpdateOrderItemRequest> OrderItems
);