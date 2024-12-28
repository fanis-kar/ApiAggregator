using ApiAggregator.Models.ExternalServices;

namespace ApiAggregator.Extensions;

public static class ConfigurationExtensions
{
    public static void AddExternalApiSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ExternalApiSettings>(configuration.GetSection("ExternalServices"));
    }
}