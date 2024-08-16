using Microsoft.Extensions.DependencyInjection;
using VinylShop.Application.Auth;
using VinylShop.Infrastructure.Authentication;

namespace VinylShop.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
