using VinylShop.API.Contracts.Orders;

namespace VinylShop.API.Contracts.Users;

public record GetUserResponse(
    Guid UserId,
    string PasswordHash, 
    string Email
);