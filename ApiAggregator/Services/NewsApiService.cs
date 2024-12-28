using ApiAggregator.Models.ExternalServices;
using ApiAggregator.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ApiAggregator.Services;

public class NewsApiService : INewsApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly string _apiKey;
    private readonly ILogger<NewsApiService> _logger;

    public NewsApiService(HttpClient httpClient, 
            IOptions<ExternalApiSettings> options,
            ILogger<NewsApiService> logger)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation/1.0");

        var settings = options.Value.NewsApi;
        _apiUrl = settings.Url ?? string.Empty;
        _apiKey = settings.ApiKey ?? string.Empty;
    }

    public async Task<NewsApiResponse> GetData(string query)
    {
        if (string.IsNullOrEmpty(query)) query = "everything";
        query = Uri.EscapeDataString(query);

        var requestUrl = $"{_apiUrl}everything?q={query}&apiKey={_apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NewsApiResponse>(content) ?? new();
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
}