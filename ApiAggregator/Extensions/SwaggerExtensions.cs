using Microsoft.OpenApi.Models;

namespace ApiAggregator.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API Aggregator",
            Description = "This API Aggregator consolidates data from multiple external APIs (newsapi, openweathermap, restcountries) <br> and provides a unified endpoint to access the aggregated information",
            Version = "v1",
            Contact = new OpenApiContact() { Name = "Theofanis (Fanis) Karamichalelis", Email = "fanis.karamichalelis@gmail.com" },
            License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        }));

        services.AddApiVersioning(
            options =>
            {
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    public static void UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        if (!app.ApplicationServices.GetRequiredService<IHostEnvironment>().IsDevelopment()) return;

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = "API Aggregator";
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Aggregator v1 (Default)");
            c.RoutePrefix = string.Empty;
        });
    }
}