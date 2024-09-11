using VinylShop.Core.Enums;

namespace VinylShop.Core.Interfaces.Services;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
}
