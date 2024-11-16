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
using Stripe;
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
builder.Services.AddControllers();

var services = builder.Services;
var configuration = builder.Configuration;

DotNetEnv.Env.Load();

var stripeKey = configuration["Stripe:SecretKey"];
if (string.IsNullOrEmpty(stripeKey))
{
    Log.Error("Stripe Secret Key is not set or is empty");
}
else
{
    Log.Information("Stripe Secret Key successfully loaded");
}

configuration["Stripe:SecretKey"] = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
configuration["Stripe:PublicKey"] = Environment.GetEnvironmentVariable("STRIPE_PUBLIC_KEY");


services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd",
        corsPolicyBuilder => corsPolicyBuilder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()   
            .AllowCredentials());
});

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

services.AddHttpClient();

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
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.None,
    Secure = CookieSecurePolicy.Always
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

app.UseSerilogRequestLogging();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Cross-Origin-Opener-Policy", "same-origin");
    context.Response.Headers.Add("Cross-Origin-Embedder-Policy", "require-corp");
    await next();
});

app.UseHttpsRedirection();

app.UseCors("AllowFrontEnd");

app.UseAuthentication();
app.UseAuthorization();

app.AddMappedEndpoints();

app.Run();

