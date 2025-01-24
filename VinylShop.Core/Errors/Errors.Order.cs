using ErrorOr;

namespace VinylShop.Core.Errors;

public static partial class Errors
{
    public static class Order
    {
        public static Error NotFound => Error.NotFound(
            code: "Order.NotFound",
            description: "The specified order was not found.");

        public static Error InvalidStatus => Error.Validation(
            code: "Order.InvalidStatus",
            description: "The order status is invalid.");
    }
}