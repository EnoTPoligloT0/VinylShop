using System.ComponentModel.DataAnnotations;

namespace VinylShop.API.Contracts.Users;

public record LoginUserRequest
(
    [Required] string Email,
    [Required] string Password
);