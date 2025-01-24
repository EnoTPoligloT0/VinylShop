using ErrorOr;

namespace VinylShop.Core.Errors;

public static partial class  Errors
{
    public static class Payment
    {
        public static Error NotFound => Error.NotFound(
            code: "Payment.NotFound", 
            description: "The specified payment was not found.");

        public static Error InvalidAmount => Error.Validation(
            code: "Payment.InvalidAmount", 
            description: "The payment amount must be greater than zero.");

        public static Error InvalidMethod => Error.Validation(
            code: "Payment.InvalidMethod", 
            description: "The payment method is not valid.");
    }
}