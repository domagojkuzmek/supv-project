using FluentValidation;
using Supv.Src.Supv.Contracts;

namespace Supv.Src.Supv.Validators;

public class ReplaceVehicleDtoValidator : AbstractValidator<ReplaceVehicleDto>
{
    public ReplaceVehicleDtoValidator()
    {
        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.VehicleCategory)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.Brand)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.PurchaseDate)
            .NotEmpty()
            .NotEqual(DateTime.MinValue)
            .LessThanOrEqualTo(DateTime.UtcNow);

        RuleFor(x => x.PurchaseType)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.Status)
            .NotEmpty()
            .IsInEnum();
    }
}
