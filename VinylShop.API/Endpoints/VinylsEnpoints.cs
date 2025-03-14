using Microsoft.AspNetCore.Mvc;
using VinylShop.API.Contracts.Pagination;
using VinylShop.API.Contracts.Vinyls;
using VinylShop.API.Extensions;
using VinylShop.Application.Services;
using VinylShop.Core.Enums;
using VinylShop.Core.Models;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace VinylShop.API.Endpoints;

public static class VinylsEnpoints
{
    public static IEndpointRouteBuilder MapVinylEnpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("vinyls");

        endpoints.MapPost("/", CreateVinyl)
            .RequirePermissions(Permission.Create)
            .DisableAntiforgery();

        endpoints.MapGet("/", GetVinyls)
            .RequirePermissions(Permission.Read)
            .AllowAnonymous();

        endpoints.MapGet("/{id:guid}", GetVinylById)
            .RequirePermissions(Permission.Read)
            .AllowAnonymous();

        endpoints.MapGet("orderItems/{orderItemId:guid}", GetVinylByOrderItemId)
            .RequirePermissions(Permission.Read);

        endpoints.MapPut("/{id:guid}", UpdateVinyl)
            .RequirePermissions(Permission.Update);

        endpoints.MapDelete("/{id:guid}", DeleteVinyl)
            .RequirePermissions(Permission.Delete);

        endpoints.MapPost("/{vinylId:guid}/upload-image", UploadVinylImage)
            .RequirePermissions(Permission.Create)
            .DisableAntiforgery();

        endpoints.MapGet("/search", SearchVinyls)
            .AllowAnonymous();

        endpoints.MapGet("/filter", GetFilteredVinyls)
            .AllowAnonymous();

        return endpoints;
    }

    private static async Task<IResult> CreateVinyl(
        [FromForm] CreateVinylRequest request,
        IFormFile ImageFile,
        VinylService vinylService)
    {
        if (request == null)
        {
            return Results.BadRequest("Request cannot be null.");
        }

        if (ImageFile == null || ImageFile.Length == 0)
        {
            return Results.BadRequest("Image file is required.");
        }

        byte[] imageData;
        using (var memoryStream = new MemoryStream())
        {
            await ImageFile.CopyToAsync(memoryStream);
            imageData = memoryStream.ToArray();
        }

        var vinylResult = Vinyl.Create(
            Guid.NewGuid(),
            request.Title,
            request.Artist,
            request.Genre,
            request.ReleaseYear,
            request.Price,
            request.Stock,
            request.Description,
            request.IsAvailable,
            Convert.ToBase64String(imageData)
        );

        if (vinylResult.IsFailure)
        {
            return Results.BadRequest(vinylResult.Error);
        }

        var vinyl = vinylResult.Value;
        await vinylService.CreateVinyl(vinyl);

        return Results.Ok(vinyl);
    }

    private static async Task<IResult> UploadVinylImage(
        [FromRoute] Guid vinylId,
        IFormFile imageFile,
        VinylService vinylService)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            return Results.BadRequest("Image file is required.");
        }

        byte[] imageData;
        using (var memoryStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(memoryStream);
            imageData = memoryStream.ToArray();
        }

        string imageBase64 = Convert.ToBase64String(imageData);

        var vinylResult = await vinylService.UpdateVinylImage(vinylId, imageBase64);

        if (vinylResult.IsFailure)
        {
            return Results.BadRequest(vinylResult.Error);
        }

        return Results.Ok();
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
                v.IsAvailable,
                Convert.ToBase64String(v.Image)
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
            vinyl.IsAvailable,
            Convert.ToBase64String(vinyl.Image)
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
            vinyl.IsAvailable,
            Convert.ToBase64String(vinyl.Image)
        );

        return Results.Ok(response);
    }

    private static async Task<IResult> SearchVinyls(
        [FromQuery] string searchTerm,
        VinylService vinylService)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Results.BadRequest("Search term cannot be empty.");
        }

        var vinyls = await vinylService.SearchVinyls(searchTerm);

        var response = vinyls.Select(v => new GetVinylResponse
        (
            v.Id,
            v.Title,
            v.Artist,
            v.Genre,
            v.ReleaseYear,
            v.Price,
            v.Stock,
            v.Description,
            v.IsAvailable,
            Convert.ToBase64String(v.Image)
        ));

        return Results.Ok(response);
    }
    private static async Task<IResult> GetFilteredVinyls(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string? genre,
        [FromQuery] int? decade,
        [FromQuery] string? sortOption,
        VinylService vinylService)
    {
        var vinyls = await vinylService.GetFilteredVinyls(genre, decade, sortOption, page, pageSize);
        var totalVinyls = await vinylService.GetTotalVinylCount();
        var response = new GetVinylsResponsePagination(
            vinyls.Select(v => new GetVinylResponse
            (
                v.Id,
                v.Title,
                v.Artist,
                v.Genre,
                v.ReleaseYear,
                v.Price,
                v.Stock,
                v.Description,
                v.IsAvailable,
                Convert.ToBase64String(v.Image)
            )),
            new PaginationInfo(
                page,
                pageSize,
                totalVinyls,
                (int)Math.Ceiling(totalVinyls / (double)pageSize)
            ));

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