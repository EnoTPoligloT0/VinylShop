namespace VinylShop.API.Contracts.Users;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string? PhoneNumber,
    string? AddressLine1,
    string? AddressLine2,
    string? City,
    string? State,
    string? ZipCode
);