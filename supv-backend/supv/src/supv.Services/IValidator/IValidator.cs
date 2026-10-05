using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Supv.Src.Supv.Contracts;
using Supv.Src.Supv.Validators;

namespace Supv.Src.Supv.Services;

public static class IValidator
{
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateVehicleDtoValidator>();

        return services;
    }
}
