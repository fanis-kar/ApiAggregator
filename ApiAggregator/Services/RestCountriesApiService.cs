using ApiAggregator.Models.ExternalServices;
using ApiAggregator.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ApiAggregator.Services;

public class RestCountriesApiService : IRestCountriesApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly ILogger<RestCountriesApiService> _logger;

    public RestCountriesApiService(HttpClient httpClient, 
            IOptions<ExternalApiSettings> options,
            ILogger<RestCountriesApiService> logger)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ApiAggregation/1.0");

        var settings = options.Value.RestCountriesApi;
        _apiUrl = settings.Url ?? string.Empty;
    }

    public async Task<RestCountriesApiResponse> GetData(string query)
    {
        if (string.IsNullOrEmpty(query)) query = "euro";
        var requestUrl = $"{_apiUrl}{query}";
        try
        {
            var response = await _httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var countries = JsonConvert.DeserializeObject<List<Country>>(content);

            return new RestCountriesApiResponse { Countries = countries ?? [] };
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
}