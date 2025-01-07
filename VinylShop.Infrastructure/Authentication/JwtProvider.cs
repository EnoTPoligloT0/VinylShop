using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VinylShop.Application.Auth;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Core.Models;

namespace VinylShop.Infrastructure.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IPermissionService _permissionService;

    public JwtProvider(IOptions<JwtOptions> options, IPermissionService permissionService)
    {
        _permissionService = permissionService;
        _options = options.Value;
    }

    public string Generate(User user)
    {
        var permissions =  _permissionService.GetPermissionsAsync(user.UserId).Result;

        string role = permissions.Count == 1 ? "User" : "Admin";


        Claim[] claims =
        [
            new("userId", user.UserId.ToString()),
            new ("Role", role)
        ];
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}