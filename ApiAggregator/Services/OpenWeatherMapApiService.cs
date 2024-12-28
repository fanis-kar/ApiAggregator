using ApiAggregator.Models.ExternalServices;
using ApiAggregator.Services.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ApiAggregator.Services;

public class OpenWeatherMapApiService : IOpenWeatherMapApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly string _apiKey;
    private readonly IMemoryCache _cache;
    private readonly ILogger<OpenWeatherMapApiService> _logger;
    private readonly IStatisticsService _statistics;

    public OpenWeatherMapApiService(HttpClient httpClient, 
            IOptions<ExternalApiSettings> options,
            IMemoryCache cache,
            ILogger<OpenWeatherMapApiService> logger,
            IStatisticsService statistics)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation/1.0");

        _cache = cache;
        _logger = logger;
        _statistics = statistics;

        var settings = options.Value.OpenWeatherMapApi;
        _apiUrl = settings.Url ?? string.Empty;
        _apiKey = settings.ApiKey ?? string.Empty;
    }

    public async Task<OpenWeatherMapApiResponse> GetData(string query)
    {
        var stopwatch = Stopwatch.StartNew();

        if (string.IsNullOrEmpty(query)) query = "Galatsi,GR";
        query = Uri.EscapeDataString(query);

        var cacheKey = $"OpenWeatherMapApiCache_{query}";
        if (_cache.TryGetValue(cacheKey, out OpenWeatherMapApiResponse? openWeatherMapApiResponse))
        {
            LogRequest(stopwatch);
            return openWeatherMapApiResponse ?? new();
        }

        var requestUrl = $"{_apiUrl}?q={query}&appid={_apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            openWeatherMapApiResponse = JsonConvert.DeserializeObject<OpenWeatherMapApiResponse>(content) ?? new();

            LogRequest(stopwatch);

            _cache.Set(cacheKey, openWeatherMapApiResponse, TimeSpan.FromMinutes(5));
            return openWeatherMapApiResponse;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"OpenWeatherMapApi error: {ex.Message}");
            return new OpenWeatherMapApiResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error fetching OpenWeatherMapApi data: {ex.Message}");
            return new OpenWeatherMapApiResponse();
        }
    }

    private void LogRequest(Stopwatch stopwatch)
    {
        stopwatch.Stop();
        _statistics.LogRequest(nameof(OpenWeatherMapApiService), stopwatch.ElapsedMilliseconds);
    }
}