using Microsoft.EntityFrameworkCore;
using Serilog;
using VinylShop.Application.Services;
using VinylShop.Core.Interfaces.Repositories;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Application;
using VinylShop.Infrastructure;
using VinylShop.DataAccess;
using VinylShop.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var services = builder.Services; 


services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddDbContext<VinylShopDbContext>();


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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

