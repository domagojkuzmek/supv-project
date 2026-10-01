using FluentValidation;
using Supv.Src.Supv.Contracts;

namespace Supv.Src.Supv.Validators;

public class UpdateVehicleDtoValidator : AbstractValidator<UpdateVehicleDto>
{
    public UpdateVehicleDtoValidator()
    {
        RuleFor(x => x.RegistrationNumber)
            .MaximumLength(20)
            .When(x => x.RegistrationNumber is not null);

        RuleFor(x => x.VehicleCategory)
            .IsInEnum()
            .When(x => x.VehicleCategory.HasValue);

        RuleFor(x => x.Brand)
            .MaximumLength(100)
            .When(x => x.Brand is not null);

        RuleFor(x => x.Model)
            .MaximumLength(100)
            .When(x => x.Model is not null);

        RuleFor(x => x.PurchaseDate)
            .NotEqual(DateTime.MinValue)
            .When(x => x.PurchaseDate.HasValue);

        RuleFor(x => x.PurchaseType)
            .IsInEnum()
            .When(x => x.PurchaseType.HasValue);

        RuleFor(x => x.Status)
            .IsInEnum()
            .When(x => x.Status.HasValue);
    }
}
