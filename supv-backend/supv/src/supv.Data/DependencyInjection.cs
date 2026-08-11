using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace supv.src.supv.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}