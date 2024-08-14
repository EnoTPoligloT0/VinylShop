using VinylShop.API.Contracts.Orders;

namespace VinylShop.API.Contracts.Users;

public record GetUserResponse(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string? AddressLine1,
    string? AddressLine2,
    string? City,
    string? State,
    string? ZipCode,
    List<GetOrderResponse>? Orders
);