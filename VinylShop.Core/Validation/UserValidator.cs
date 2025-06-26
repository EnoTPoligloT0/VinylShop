using FluentValidation;
using VinylShop.Core.Models;

namespace VinylShop.Core.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email format is invalid");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Password is required");
    }
}