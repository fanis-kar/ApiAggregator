namespace ApiAggregator.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "API Aggregation",
            Description = "This API aggregation service consolidates data from multiple external APIs (newsapi, openweathermap, restcountries) <br> and provides a unified endpoint to access the aggregated information"
        }));
    }

    public static void UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        if (app.ApplicationServices.GetRequiredService<IHostEnvironment>().IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "API Aggregation";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggregation API V1");

            });
        }

        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("/swagger");
                return;
            }
            await next();
        });
    }
}