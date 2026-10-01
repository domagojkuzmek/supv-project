using System.Text.RegularExpressions;
using FluentValidation;
using Supv.Src.Supv.Contracts;

namespace Supv.Src.Supv.Validators;

public class CreateVehicleDtoValidator : AbstractValidator<CreateVehicleDto>
{
    public CreateVehicleDtoValidator()
    {
        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .WithMessage("Registration number is required.")
            .Matches(@"^[A-Z]{2}-\d{4}-[A-Z]{2}$", RegexOptions.IgnoreCase)
            .WithMessage("Registration number must be in the format 'ZG-1234-GF'.");

        RuleFor(x => x.VehicleCategory)
            .IsInEnum()
            .WithMessage("Invalid category type");

        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("Brand is required.")
            .MaximumLength(100)
            .WithMessage("Brand must not exceed 100 characters.");

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage("Model is required.")
            .MaximumLength(100)
            .WithMessage("Model must not exceed 100 characters.");

        RuleFor(x => x.PurchaseDate)
            .NotEmpty()
            .WithMessage("Purchase date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Purchase date cannot be in the future.");

        RuleFor(x => x.PurchaseType)
            .IsInEnum()
            .WithMessage("Invalid purchase type.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid status.");
    }
}
