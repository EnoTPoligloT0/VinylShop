using VinylShop.API.Endpoints;

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
}