using Microsoft.EntityFrameworkCore;
using Serilog;
using VinylShop.API.Endpoints;
using VinylShop.Application.Services;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Application;
using AutoMapper;
using Microsoft.OpenApi.Models;
using VinylShop.Infrastructure;
using VinylShop.DataAccess;
using VinylShop.DataAccess.Repositories;
using VinylShop.DataAccess.Mappings;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var services = builder.Services; 

services.AddAutoMapper(typeof(DataBaseMappings)); 

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VinylShop API", Version = "v1" });
    // Дополнительные настройки Swagger, если необходимо
});

services.AddDbContext<VinylShopDbContext>();
services.AddAuthorization();

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


app.UseAuthorization();
app.MapUsersEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

