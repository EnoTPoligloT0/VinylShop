using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task Add(User user);
    Task Delete(Guid id);
    Task<List<User>> Get();
    Task<User> GetById(Guid id);
    Task<User?> GetByEmail(string email);
    Task Update(Guid id, string firstName, string lastName, string? phoneNumber, string? addressLine1,
        string? addressLine2, string? city, string? state, string? zipCode);

    Task UpdatePassword(string password);
    
    Task<HashSet<Enums.Permission>> GetUserPermissions(Guid userId);
}