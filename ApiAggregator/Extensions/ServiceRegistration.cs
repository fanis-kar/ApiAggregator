﻿using ApiAggregator.Services;
using ApiAggregator.Services.Interfaces;

namespace ApiAggregator.Extensions;

public static class ServiceRegistration
{
    public static void AddExternalApiServices(this IServiceCollection services)
    {
        services.AddHttpClient<INewsApiService, NewsApiService>();
        services.AddHttpClient<IOpenWeatherMapApiService, OpenWeatherMapApiService>();
        services.AddHttpClient<IRestCountriesApiService, RestCountriesApiService>();

        services.AddScoped<IApiAggregationService, ApiAggregationService>();

        services.AddSingleton<IStatisticsService, StatisticsService>();
    }

    public static void AddCommonServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddMemoryCache();
    }
}