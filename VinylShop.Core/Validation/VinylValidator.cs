using FluentValidation;
using VinylShop.Core.Models;

namespace VinylShop.Core.Validation;

public class VinylValidator : AbstractValidator<Vinyl>
{
    public VinylValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(Errors.Errors.Vinyl.NotFound.Description)
            .MaximumLength(100);

        RuleFor(x => x.Artist)
            .NotEmpty().WithMessage("The artist of the vinyl is required.")
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("The price must be greater than zero.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("The stock must be zero or greater.");

        RuleFor(x => x.ReleaseYear)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithMessage($"The release year must be between 1900 and {DateTime.Now.Year}.");
    }
}