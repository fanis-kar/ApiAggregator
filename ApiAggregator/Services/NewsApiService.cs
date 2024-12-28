using ApiAggregator.Models.ExternalServices;
using ApiAggregator.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ApiAggregator.Services;

public class NewsApiService : INewsApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly string _apiKey;
    private readonly IMemoryCache _cache;
    private readonly ILogger<NewsApiService> _logger;
    private readonly IStatisticsService _statistics;

    public NewsApiService(HttpClient httpClient, 
            IOptions<ExternalApiSettings> options,
            IMemoryCache cache,
            ILogger<NewsApiService> logger,
            IStatisticsService statistics)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation/1.0");

        _cache = cache;
        _logger = logger;
        _statistics = statistics;

        var settings = options.Value.NewsApi;
        _apiUrl = settings.Url ?? string.Empty;
        _apiKey = settings.ApiKey ?? string.Empty;
    }

    public async Task<NewsApiResponse> GetData(string query)
    {
        var stopwatch = Stopwatch.StartNew();

        if (string.IsNullOrEmpty(query)) query = "everything";
        query = Uri.EscapeDataString(query);

        var cacheKey = $"NewsApiCache_{query}";
        if (_cache.TryGetValue(cacheKey, out NewsApiResponse? newsApiResponse))
        {
            LogRequest(stopwatch);
            return newsApiResponse ?? new();
        }

        var requestUrl = $"{_apiUrl}everything?q={query}&apiKey={_apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            newsApiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(content) ?? new();

            LogRequest(stopwatch);

            _cache.Set(cacheKey, newsApiResponse, TimeSpan.FromMinutes(5));
            return newsApiResponse;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"NewsApi error: {ex.Message}");
            return new NewsApiResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error fetching NewsApi data: {ex.Message}");
            return new NewsApiResponse();
        }
    }

    private void LogRequest(Stopwatch stopwatch)
    {
        stopwatch.Stop();
        _statistics.LogRequest(nameof(NewsApiService), stopwatch.ElapsedMilliseconds);
    }
}