using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace supv.src.supv.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionStringValue)
    {

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionStringValue));

        return services;
    }
}