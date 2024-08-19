using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VinylShop.API.Contracts.Vinyls;
using VinylShop.Application.Services;
using VinylShop.Core.Models;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace VinylShop.API.Endpoints;

public static class VinylsEnpoints
{
    public static IEndpointRouteBuilder MapVinylEnpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("vinyls");
        
        endpoints.MapPost("/", CreateVinyl);
        endpoints.MapGet("/", GetVinyls);
        endpoints.MapGet("/{id:guid}", GetVinylById);
        endpoints.MapPut("/{id:guid}", UpdateVinyl);
        endpoints.MapDelete("/{id:guid}", DeleteVinyl);

        return endpoints;
    }
    
    private static async Task<IResult> CreateVinyl(
        [FromBody] CreateVinylRequest request,
        VinylService vinylService)
    {
        var vinylResult = Vinyl.Create(
            Guid.NewGuid(),
            request.Title,
            request.Artist,
            request.Genre,
            request.ReleaseYear,
            request.Price,
            request.Stock,
            request.Description,
            request.IsAvailable
        );

        if (!vinylResult.IsSuccess) return Results.BadRequest(vinylResult.Error);
        
        var vinyl = vinylResult.Value;
            
        await vinylService.CreateVinyl(vinyl);
            
        return Results.Ok(vinyl);
    }

    private static async Task<IResult> GetVinyls(
        [FromBody] GetVinylResponse request,
        VinylService vinylService)
    {
        var vinyls = await vinylService.GetVinyls();
        
        var response = vinyls.Select(v => new GetVinylResponse
        {
            Id = v.Id,
            Title = v.Title,
            Artist = v.Artist,
            Genre = v.Genre,
            ReleaseYear = v.ReleaseYear,
            Price = v.Price,
            Stock = v.Stock,
            Description = v.Description,
            IsAvailable = v.IsAvailable
        });

        return Results.Ok(vinyls);
    }

    private static async Task<IResult> GetVinylById(
        [FromRoute] Guid id,
        VinylService vinylService)
    {
        var vinyl = await vinylService.GetVinylById(id);

        var response = new GetVinylResponse
        {
            Id = vinyl.Id,
            Title = vinyl.Title,
            Artist = vinyl.Artist,
            Genre = vinyl.Genre,
            ReleaseYear = vinyl.ReleaseYear,
            Price = vinyl.Price,
            Stock = vinyl.Stock,
            Description = vinyl.Description,
            IsAvailable = vinyl.IsAvailable
        };

        return Results.Ok(response);
    }

  
    private static async Task<IResult> UpdateVinyl(
        [FromRoute] Guid id,
        [FromBody] UpdateVinylRequest request,
        VinylService vinylService)
    {
        await vinylService.UpdateVinyl(
            id,
            request.Title,
            request.Artist,
            request.Genre,
            request.ReleaseYear,
            request.Price,
            request.Stock,
            request.Description,
            request.IsAvailable
        );

        return Results.Ok();
    }

    private static async Task<IResult> DeleteVinyl(
        [FromRoute] Guid id,
        VinylService vinylService)
    {
        await vinylService.DeleteVinyl(id);

        return Results.Ok();
    }
}