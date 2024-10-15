using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VinylShop.API.Endpoints;
using VinylShop.Application.Services;
using VinylShop.Core.Enums;
using VinylShop.Core.Interfaces.Services;
using VinylShop.Infrastructure;
using VinylShop.Infrastructure.Authentication;

namespace VinylShop.API.Extensions;

public static class ApiExtensions
{
    public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapUsersEndpoints();
        app.MapOrderItemsEndpoints();
        app.MapVinylEnpoints();
        app.MapOrderEndpoints();
        app.MapShipmentEndpoints();
        app.MapPaymentEndpoints();
    }
    
    public static void AddApiAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["secretCookie"];
                        return Task.CompletedTask;
                    }
                };
            });
            // .AddCookie("Cookies") 
            // .AddGoogle(options =>
            // {
            //     options.ClientId = configuration["Authentication:Google:ClientId"];
            //     options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            //     options.CallbackPath = new PathString("/signin-google");
            // });

        services.AddAuthorization();

        services.AddScoped<IPermissionService, PermissionService>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                policy.Requirements.Add(new PermissionRequirement(new[] { Permission.Create }));
            });
        });
    }

    public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
        this TBuilder builder, params Permission[] permissions)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(pb =>
            pb.AddRequirements(new PermissionRequirement(permissions)));
    }
}
