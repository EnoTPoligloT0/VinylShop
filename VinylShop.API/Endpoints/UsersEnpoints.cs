using Microsoft.AspNetCore.Mvc;
using VinylShop.API.Contracts.Users;
using VinylShop.Application.Services;

namespace VinylShop.API.Endpoints;

public static class UsersEnpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);

        app.MapPost("login", Login);

        return app;
    }

    private static async Task<IResult> Login(
        LoginUserRequest request,
        UserService userService)
    {
        var token = await userService.Login(request.Email, request.Password);

        return Results.Ok(token);
    }

    private static async Task<IResult> Register(
        [FromBody] RegisterUserRequest request,
        UserService usersService)
    {
        await usersService.Register(request.UserId, request.PasswordHash, request.Email);

        return Results.Ok();
    }
}