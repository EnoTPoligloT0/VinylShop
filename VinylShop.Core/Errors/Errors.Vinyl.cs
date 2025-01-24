using ErrorOr;

namespace VinylShop.Core.Errors;

public static partial class Errors
{
    public static class Vinyl
    {
        public static Error NotFound => Error.NotFound(
            code: "Vinyl.NotFound",
            description: "The specified vinyl not found or does not exist.");

        public static Error InsufficientStock => Error.Conflict(
            code: "Vinyl.InsufficientStock",
            description: "There is not enough stock available for this vinyl.");

        public static Error InvalidPrice => Error.Validation(
            code: "Vinyl.InvalidPrice",
            description: "The price of the vinyl must be greater than zero.");

        public static Error InvalidReleaseYear => Error.Validation(
            code: "Vinyl.InvalidReleaseYear",
            description: "The release year must be between 1900 and the current year.");
    }
}
