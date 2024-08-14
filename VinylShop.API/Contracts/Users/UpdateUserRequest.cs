using System.ComponentModel.DataAnnotations;

namespace VinylShop.API.Contracts.Users;

public record UpdateUserRequest(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] string? PhoneNumber,
    [Required] string? AddressLine1,
    [Required] string? AddressLine2,
    [Required] string? City,
    [Required] string? State,
    [Required] string? ZipCode
);