using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;
using VinylShop.API.Contracts.Shipments;
using VinylShop.Application.Services;
using VinylShop.Core.Models;

namespace VinylShop.API.Endpoints;

public static class ShipmentEndpoints
{
    public static IEndpointRouteBuilder MapShipmentEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("shipments");

        endpoints.MapPost("/", CreateShipment);
        endpoints.MapGet("/{id:guid}", GetShipmentById);
        endpoints.MapGet("/order/{orderId:guid}", GetShipmentByOrderId);
        endpoints.MapPut("/{id:guid}", UpdateShipment);
        endpoints.MapDelete("/{id:guid}", DeleteShipment);

        return endpoints;
    }

    private static async Task<IResult> CreateShipment(
        [FromRoute] Guid orderId,
        [FromBody] CreateShipmentRequest request,
        [FromServices] ShipmentService shipmentService)
    {
        var shipmentResult = Shipment.Create(
            Guid.NewGuid(),
            orderId,
            request.ShipmentDate,
            request.TrackingNumber,
            request.ShipmentStatus
        );

        if (!shipmentResult.IsSuccess) return Results.BadRequest(shipmentResult.Error);

        await shipmentService.CreateShipment(shipmentResult.Value);

        return Results.Ok(shipmentResult.Value);
    }


    private static async Task<IResult> GetShipmentById(
        [FromRoute] Guid id,
        [FromServices] ShipmentService shipmentService)
    {
        var shipment = await shipmentService.GetShipmentById(id);

        var response = new GetShipmentResponse
        (
            id,
            shipment.OrderId,
            shipment.ShipmentDate,
            shipment.TrackingNumber,
            shipment.ShipmentStatus
        );

        return Results.Ok(response);
    }

    private static async Task<IResult> GetShipmentByOrderId(
        [FromRoute] Guid orderId,
        [FromServices] ShipmentService shipmentService)
    {
        var shipment = await shipmentService.GetShipmentById(orderId);

        var response = new GetShipmentResponse
        (
            shipment.OrderId,
            shipment.OrderId,
            shipment.ShipmentDate,
            shipment.TrackingNumber,
            shipment.ShipmentStatus
        );

        return Results.Ok(response);
    }

    private static async Task<IResult> UpdateShipment(
        [FromRoute] Guid id,
        UpdateShipmentRequest request,
        [FromServices] ShipmentService shipmentService)
    {
        await shipmentService.UpdateShipment(id, request.TrackingNumber, request.ShipmentStatus);

        return Results.Ok();
    }

    private static async Task<IResult> DeleteShipment(
        [FromRoute] Guid id,
        [FromServices] ShipmentService shipmentService)
    {
        await shipmentService.DeleteShipment(id);

        return Results.Ok();
    }
}