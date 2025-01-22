using VinylShop.API.Contracts.Users;
using VinylShop.Core.Enums;

namespace VinylShop.API.Contracts.Orders;

public record GetOrdersResponse(
    Guid Id,
    Guid UserId,
    DateTime OrderDate,
    decimal TotalAmount,
    string Status
    );