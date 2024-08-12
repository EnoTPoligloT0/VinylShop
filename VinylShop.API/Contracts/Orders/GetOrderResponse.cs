using VinylShop.API.Contracts.OrderItems;
using VinylShop.API.Contracts.Payments;
using VinylShop.API.Contracts.Shipments;
using VinylShop.API.Contracts.Users;

namespace VinylShop.API.Contracts.Orders;

public record GetOrderResponse(
    Guid Id,
    Guid UserId,
    GetUserResponse GetUser,
    DateTime OrderDate,
    decimal TotalAmount,
    List<GetOrderItemResponse> OrderItems,
    List<GetPaymentResponse> Payments,
    List<GetShipmentResponse> Shipments
);