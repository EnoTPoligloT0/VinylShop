using ErrorOr;

namespace VinylShop.Core.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error NotFound => Error.NotFound(
            code: "User.NotFound", 
            description: "The specified user was not found.");

        public static Error InvalidEmail => Error.Validation(
            code: "User.InvalidEmail", 
            description: "The email address is invalid.");

        public static Error InvalidPhoneNumber => Error.Validation(
            code: "User.InvalidPhoneNumber", 
            description: "The phone number is invalid.");
    }
}