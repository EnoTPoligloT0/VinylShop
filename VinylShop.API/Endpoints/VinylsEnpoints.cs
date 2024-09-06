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
        endpoints.MapGet("orderItems/{orderItemId:guid}", GetVinylByOrderItemId);
        endpoints.MapPut("/{id:guid}", UpdateVinyl);
        endpoints.MapDelete("/{id:guid}", DeleteVinyl);

        return endpoints;
    }

    private static async Task<IResult> CreateVinyl(
        [FromBody] CreateVinylRequest request,
        HttpContext context,
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
        VinylService vinylService)
    {
        var vinyls = await vinylService.GetVinyls();

        var response = vinyls
           .Select(v => new GetVinylResponse
                   (
                       v.Id,
                       v.Title,
                       v.Artist,
                       v.Genre,
                       v.ReleaseYear,
                       v.Price,
                       v.Stock,
                       v.Description,
                       v.IsAvailable
                   ));

        return Results.Ok(response);
    }

    private static async Task<IResult> GetVinylById(
        [FromRoute] Guid id,
        VinylService vinylService)
    {
        var vinyl = await vinylService.GetVinylById(id);

        var response = new GetVinylResponse
        (
            id,
            vinyl.Title,
            vinyl.Artist,
            vinyl.Genre,
            vinyl.ReleaseYear,
            vinyl.Price,
            vinyl.Stock,
            vinyl.Description,
            vinyl.IsAvailable
        );

        return Results.Ok(response);
    }

    private static async Task<IResult> GetVinylByOrderItemId(
        [FromRoute] Guid orderItemId,
        VinylService vinylService)
    {
        var vinyl = await vinylService.GetVinylById(orderItemId);

        var response = new GetVinylByOrderItemResponse
        (
            vinyl.Id,
            orderItemId,
            vinyl.Title,
            vinyl.Artist,
            vinyl.Genre,
            vinyl.ReleaseYear,
            vinyl.Price,
            vinyl.Stock,
            vinyl.Description,
            vinyl.IsAvailable
        );

        return Results.Ok(response);
    }

    // //todo decision
    // [HttpGet("orders/{orderId:guid}/vinyls")]
    // private static async Task<IResult> GetVinylsInOrder(
    //     [FromRoute] Guid orderId,
    //     OrderItemService orderItemService,
    //     VinylService vinylService)
    // {
    //     try
    //     {
    //         // Retrieve order items
    //         var orderItems = await orderItemService.GetOrderItem(orderId);
    //         if (!orderItems.Any())
    //         {
    //             return Results.NotFound("No items found for this order.");
    //         }
    //
    //         var vinylIds = orderItems.Select(oi => oi.VinylId).Distinct();
    //
    //         var vinyls = await vinylService.GetVinyls();
    //
    //         // Map order items to vinyl details
    //         var response = orderItems.Select(oi =>
    //         {
    //             var vinyl = vinyls.FirstOrDefault(v => v.Id == oi.VinylId);
    //             return new
    //             {
    //                 OrderItemId = oi.Id,
    //                 VinylId = vinyl?.Id,
    //                 Title = vinyl?.Title,
    //                 Artist = vinyl?.Artist,
    //                 Genre = vinyl?.Genre,
    //                 ReleaseYear = vinyl?.ReleaseYear,
    //                 Price = vinyl?.Price,
    //                 Quantity = oi.Quantity,
    //                 UnitPrice = oi.UnitPrice
    //             };
    //         });
    //
    //         return Results.Ok(response);
    //     }
    //     catch (Exception ex)
    //     {
    //         return Results.Problem("An error occurred while processing your request.");
    //     }
    // }

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