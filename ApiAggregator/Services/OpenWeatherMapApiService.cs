using ApiAggregator.Models.ExternalServices;
using ApiAggregator.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace aa_api_aggregation.Services;

public class OpenWeatherMapApiService : IOpenWeatherMapApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly string _apiKey;
    private readonly ILogger<OpenWeatherMapApiService> _logger;

    public OpenWeatherMapApiService(HttpClient httpClient, 
            IOptions<ExternalApiSettings> options,
            ILogger<OpenWeatherMapApiService> logger)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation/1.0");

        var settings = options.Value.OpenWeatherMapApi;
        _apiUrl = settings.Url ?? string.Empty;
        _apiKey = settings.ApiKey ?? string.Empty;
    }

    public async Task<OpenWeatherMapApiResponse> GetData(string query)
    {
        if (string.IsNullOrEmpty(query)) query = "Galatsi,GR";
        query = Uri.EscapeDataString(query);

        var requestUrl = $"{_apiUrl}?q={query}&appid={_apiKey}";

        try
        {
            var response = await _httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OpenWeatherMapApiResponse>(content) ?? new();
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
}