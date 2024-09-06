using VinylShop.API.Contracts.Vinyls;
using VinylShop.Core.Models;

namespace VinylShop.API.Contracts.OrderItems;

public record GetOrderItemResponse
(
    Guid Id,
    Guid OrderId,
    Guid VinylId ,
    int Quantity ,
    decimal UnitPrice,
    GetVinylResponse Vinyl
);