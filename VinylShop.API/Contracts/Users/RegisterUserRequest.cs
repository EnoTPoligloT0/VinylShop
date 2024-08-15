using System.ComponentModel.DataAnnotations;
using VinylShop.Core.Models;

namespace VinylShop.API.Contracts.Users;

public record RegisterUserRequest
(
    [Required]Guid UserId, 
    [Required]string PasswordHash, 
    [Required]string Email
);