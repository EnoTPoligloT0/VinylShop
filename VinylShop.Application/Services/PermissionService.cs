using VinylShop.Core.Enums;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;

namespace VinylShop.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IUserRepository _usersRepository;

    public PermissionService(IUserRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
    {
        return _usersRepository.GetUserPermissions(userId);
    }
}