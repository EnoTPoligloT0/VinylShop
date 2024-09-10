namespace VinylShop.DataAccess;

public class AuthorizationOption
{
    public RolePermissions[] RolePermissions { get; set; } = [];
}

public class RolePermissions
{
    public string Role { get; set; } = string.Empty;

    public string[] Permissions { get; set; } = [];
}
