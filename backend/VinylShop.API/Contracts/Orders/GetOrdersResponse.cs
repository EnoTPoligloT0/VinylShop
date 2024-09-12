using VinylShop.API.Contracts.Users;

namespace VinylShop.API.Contracts.Orders;

public record GetOrdersResponse(
    Guid Id,
    Guid UserId,
    DateTime OrderDate,
    decimal TotalAmount
    );