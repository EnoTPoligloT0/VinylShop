using System.ComponentModel.DataAnnotations;
using VinylShop.Core.Models;

namespace VinylShop.API.Contracts.Users;

public record RegisterUserRequest
(
    [Required]string Email,
    [Required]string PasswordHash
);