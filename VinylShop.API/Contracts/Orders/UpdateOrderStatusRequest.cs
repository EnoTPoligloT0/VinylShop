using VinylShop.Core.Enums;

namespace VinylShop.API.Contracts.Orders;

public record UpdateOrderStatusRequest(Status status);