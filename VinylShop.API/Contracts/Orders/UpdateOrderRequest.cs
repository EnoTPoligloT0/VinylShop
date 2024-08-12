using VinylShop.API.Contracts.OrderItems;

namespace VinylShop.API.Contracts.Orders;

public record UpdateOrderRequest
(
     DateTime OrderDate,
     decimal TotalAmount,
     List<UpdateOrderItemRequest> OrderItems
);