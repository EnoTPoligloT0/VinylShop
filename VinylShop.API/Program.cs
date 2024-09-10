using Microsoft.EntityFrameworkCore;
using Serilog;
using VinylShop.API.Endpoints;
using VinylShop.Application.Services;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Application;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using VinylShop.API.Extensions;
using VinylShop.API.Infrastructure;
using VinylShop.Infrastructure;
using VinylShop.DataAccess;
using VinylShop.DataAccess.Repositories;
using VinylShop.DataAccess.Mappings;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

var services = builder.Services;
var configuration = builder.Configuration;



services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
// services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));
services.AddApiAuthentication(builder.Services.BuildServiceProvider()
    .GetRequiredService<IOptions<JwtOptions>>());

services.AddAutoMapper(typeof(DataBaseMappings)); 

services.AddEndpointsApiExplorer();

services.AddExceptionHandler<GlobalExceptionHandler>();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VinylShop API", Version = "v1" });
});


builder.Services.AddDbContext<VinylShopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetSection("Database:ConnectionStrings:DefaultConnection").Value)
        .UseLoggerFactory(LoggerFactory.Create(config => config.AddConsole()))
        .EnableSensitiveDataLogging());

services.AddAuthorization();
services.AddAuthentication();

services.AddScoped<IOrderItemRepository, OrderItemRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IPaymentRepository, PaymentRepository>();
services.AddScoped<IShipmentRepository, ShipmentRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IVinylRepository, VinylRepository>();

services
    .AddApplication()
    .AddInfrastructure();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseHttpsRedirection();

app.AddMappedEndpoints();

app.Run();

