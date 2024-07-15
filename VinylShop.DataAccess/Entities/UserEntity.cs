using VinylShop.Core.Models;

namespace VinylShop.DataAccess.Entities;

public class UserEntity
{
    public Guid UserId { get; set; }
    public string FirstName { get;  set;} = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get;  set;} = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public string? AddressLine1 { get;  set;} = string.Empty;
    public string? AddressLine2 { get;  set;} = string.Empty;
    public string? City { get;  set;} = string.Empty;
    public string? State { get;  set;} = string.Empty;
    public string? ZipCode { get;  set;} = string.Empty;
    public List<Order>? Orders { get;  set;}

}