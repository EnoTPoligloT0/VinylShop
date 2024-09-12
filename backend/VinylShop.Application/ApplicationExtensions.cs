using Microsoft.Extensions.DependencyInjection;
using VinylShop.Application.Services;

namespace VinylShop.Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<OrderItemService>();
        services.AddScoped<OrderService>();
        services.AddScoped<PaymentService>();
        services.AddScoped<ShipmentService>();
        services.AddScoped<UserService>();
        services.AddScoped<VinylService>();

        return services;
    }
}