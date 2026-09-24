using FluentValidation;

namespace Supv.Src.Supv.Contracts
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters");

            RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Surname is required")
            .Length(2, 80).WithMessage("Surname must be between 2 and 80 characters");
        }
    }
}
