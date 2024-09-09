﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylShop.DataAccess.Entities;
using Permission = VinylShop.Core.Enums.Permission;
using Role = VinylShop.Core.Enums.Role;

namespace VinylShop.DataAccess.Configurations;

public partial class RolePermissionConfiguration
    : IEntityTypeConfiguration<RolePermissionEntity>
{
    private readonly AuthorizationOptions _authorization;

    public RolePermissionConfiguration(AuthorizationOptions authorization)
    {
        _authorization = authorization;
    }

    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(r => new { r.RoleId, r.PermissionId });

        builder.HasData(ParseRolePermissions());
    }

    private RolePermissionEntity[] ParseRolePermissions()
    {
        return _authorization.RolePermissions
            .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                }))
                .ToArray();
    }
}
