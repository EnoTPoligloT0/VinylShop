namespace VinylShop.API.Contracts.OrderItems;

public record GetOrderItemResponse
(
    Guid Id,
    Guid OrderId,
    Guid VinylId ,
    int Quantity ,
    decimal UnitPrice 
);