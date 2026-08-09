using supv_backend.Web.Domain.Interfaces;
using supv_backend.Web.Infrastructure;

namespace supv_backend.Web.Configurations;

public static class ServiceConfigs
{
  public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
  {
    services.AddInfrastructureServices(builder.Configuration, logger)
            .AddMediatorSourceGen(logger);


    logger.LogInformation("{Project} services registered", "Mediator and Email Sender");

    return services;
  }


}
