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
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using VinylShop.API;
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

services.AddApiAuthentication(configuration);

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<AuthorizationOption>(configuration.GetSection(nameof(AuthorizationOption)));


services.AddAutoMapper(typeof(DataBaseMappings)); 

services.AddEndpointsApiExplorer();

services.AddExceptionHandler<GlobalExceptionHandler>();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VinylShop API", Version = "v1" });
    c.EnableAnnotations(); 
    c.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary" // This is important for file uploads
    });
});


builder.Services.AddDbContext<VinylShopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetSection("Database:ConnectionStrings:VinylShopDbContext").Value)
        .UseLoggerFactory(LoggerFactory.Create(config => config.AddConsole()))
        .EnableSensitiveDataLogging());

services.AddAuthorization();
services.AddAuthentication();

services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10MB limit
});

services.AddScoped<IOrderItemRepository, OrderItemRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<IPaymentRepository, PaymentRepository>();
services.AddScoped<IShipmentRepository, ShipmentRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IVinylRepository, VinylRepository>();

services
    .AddPersistence(configuration)
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddProblemDetails();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage(); 

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseRouting(); 

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseHttpsRedirection();

app.AddMappedEndpoints();

app.Run();

