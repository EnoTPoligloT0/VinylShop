using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VinylShop.Application;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.UnitOfWork;
using VinylShop.DataAccess.Repositories;

namespace VinylShop.DataAccess
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<VinylShopDbContext>(options =>
                options.UseNpgsql(configuration.GetSection("Database:ConnectionStrings:VinylShopDbContext").Value)
                    .UseLoggerFactory(LoggerFactory.Create(config => config.AddConsole()))
                    .EnableSensitiveDataLogging());

            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IShipmentRepository, ShipmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVinylRepository, VinylRepository>();

            services.AddScoped<ITransactionManager, TransactionManager>();

            return services;
        }
    }
}