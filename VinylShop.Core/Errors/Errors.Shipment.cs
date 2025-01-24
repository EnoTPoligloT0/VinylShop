using ErrorOr;

namespace VinylShop.Core.Errors;

public static partial class Errors
{
    public static class Shipment
    {
        public static Error NotFound => Error.NotFound(
            code: "Shipment.NotFound",
            description: "The specified shipment was not found.");

        public static Error NotPaid => Error.NotFound(
            code: "Shipment.NotPaid",
            description: "Cannot create shipment for unpaid order");

        public static Error InvalidTrackingNumber => Error.Validation(
            code: "Shipment.InvalidTrackingNumber",
            description: "The shipment tracking number is invalid.");
    }
}