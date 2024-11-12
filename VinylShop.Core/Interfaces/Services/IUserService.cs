using CSharpFunctionalExtensions;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Services;

public interface IUserService
{
    Task<string> Login(string email, string password);
    Task<Result> Register(string passwordHash, string email);
    Task<List<User>> GetUsers();
    Task<User> GetUserById(Guid id);
    Task<User> GetUserByEmail(string email);
}