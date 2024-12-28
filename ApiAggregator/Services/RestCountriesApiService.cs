using ApiAggregator.Models.ExternalServices;
using ApiAggregator.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ApiAggregator.Services;

public class RestCountriesApiService : IRestCountriesApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly IMemoryCache _cache;
    private readonly ILogger<RestCountriesApiService> _logger;
    private readonly IStatisticsService _statistics;

    public RestCountriesApiService(HttpClient httpClient, 
            IOptions<ExternalApiSettings> options,
            IMemoryCache cache,
            ILogger<RestCountriesApiService> logger,
            IStatisticsService statistics)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation/1.0");

        _cache = cache;
        _logger = logger;
        _statistics = statistics;

        var settings = options.Value.RestCountriesApi;
        _apiUrl = settings.Url ?? string.Empty;
    }

    public async Task<RestCountriesApiResponse> GetData(string query)
    {
        var stopwatch = Stopwatch.StartNew();

        if (string.IsNullOrEmpty(query)) query = "euro";

        var cacheKey = $"RestCountriesApiCache_{query}";
        if (_cache.TryGetValue(cacheKey, out RestCountriesApiResponse? restCountriesApiResponse))
        {
            LogRequest(stopwatch);
            return restCountriesApiResponse ?? new();
        }

        var requestUrl = $"{_apiUrl}{query}";

        try
        {
            var response = await _httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var countries = JsonConvert.DeserializeObject<List<Country>>(content);
            restCountriesApiResponse = new RestCountriesApiResponse { Countries = countries ?? [] };

            LogRequest(stopwatch);

            _cache.Set(cacheKey, restCountriesApiResponse, TimeSpan.FromMinutes(5));
            return restCountriesApiResponse;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"RestCountriesApi error: {ex.Message}");
            return new RestCountriesApiResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error fetching RestCountriesApi data: {ex.Message}");
            return new RestCountriesApiResponse();
        }
    }

    private void LogRequest(Stopwatch stopwatch)
    {
        stopwatch.Stop();
        _statistics.LogRequest(nameof(RestCountriesApiService), stopwatch.ElapsedMilliseconds);
    }
}