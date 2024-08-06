using VinylShop.Core.Dtos.UserDtos;
using VinylShop.Core.Models;

namespace VinylShop.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task Create(User user);
    Task Delete(Guid id);
    Task<List<User>> Get();
    Task<User> GetById(Guid id);

    Task Update(Guid id, string firstName, string lastName, string? phoneNumber, string? addressLine1,
        string? addressLine2, string? city, string? state, string? zipCode);
}