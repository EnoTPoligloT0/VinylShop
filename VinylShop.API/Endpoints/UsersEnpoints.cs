using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VinylShop.API.Contracts.Users;
using VinylShop.Application.Services;

namespace VinylShop.API.Endpoints;

public static class UsersEnpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register)
            .AllowAnonymous();

        app.MapPost("login", Login)
            .AllowAnonymous();

        app.MapGet("users/email/{email}", GetByEmail);

        app.MapGet("users/{id:guid}", GetById);

        app.MapGet("users", GetUsers);

        return app;
    }
    
    private static async Task<IResult> Login(
        LoginUserRequest request,
        UserService userService,
        HttpContext context)
    {
        var token = await userService.Login(request.Email, request.Password);
        
        context.Response.Cookies.Append("secretCookie", token );
        
        return Results.Ok(token);
    }

    private static async Task<IResult> Register(
        [FromBody] RegisterUserRequest request,
        UserService usersService)
    {
        await usersService.Register(request.Email, request.Password);

        return Results.Ok();
    }

    private static async Task<IResult> GetByEmail(
        [FromRoute] string email,
        UserService userService)
    {
        var user = await userService.GetUserByEmail(email);

        var response = new GetUserResponse(user.UserId, user.Email, user.PasswordHash);

        return Results.Ok(response);
    }
    private static async Task<IResult> GetById(
        [FromRoute] Guid id,
        UserService userService)
    {
        var user = await userService.GetUserById(id);

        var response = new GetUserResponse(id, user.Email, user.PasswordHash);

        return Results.Ok(response);
    }

    private static async Task<IResult> GetUsers(
            UserService userService,
            HttpContext context
        )
    {
        var users = await userService.GetUsers();

        var response = users.Select
            (u => new GetUserResponse(u.UserId, u.Email, u.PasswordHash));

        return Results.Ok(response);
    }
}