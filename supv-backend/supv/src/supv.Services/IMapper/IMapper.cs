using Microsoft.Extensions.DependencyInjection;

namespace Supv.Src.Supv.Services;

public static class IMapper
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
        return services;
    }
}
