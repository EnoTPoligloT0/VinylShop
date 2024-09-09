using Microsoft.AspNetCore.Authorization;
using VinylShop.Core.Enums;

namespace VinylShop.Infrastructure.Authentication;

public class PermissionRequirement(Permission[] permissions)
    : IAuthorizationRequirement
{
    public Permission[] Permissions { get; set; } = permissions;
}