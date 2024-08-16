using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IUserService
{
    Task<string> Login(string email, string password);
    
    Task Register(Guid userId, string passwordHash, string email);
}