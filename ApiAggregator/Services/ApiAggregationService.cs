using ApiAggregator.Models.ExternalServices;
using ApiAggregator.Services.Interfaces;

namespace ApiAggregator.Services;

public class ApiAggregationService : IApiAggregationService
{
    private readonly INewsApiService _newsApiService;
    private readonly IOpenWeatherMapApiService _openWeatherMapApiService;
    private readonly IRestCountriesApiService _restCountriesApiService;

    public ApiAggregationService(INewsApiService newsApiService, 
        IOpenWeatherMapApiService openWeatherMapApiService, 
        IRestCountriesApiService restCountriesApiService)
    {
        _newsApiService = newsApiService;
        _openWeatherMapApiService = openWeatherMapApiService;
        _restCountriesApiService = restCountriesApiService;
    }

    public async Task<AggregatedApiResponse> GetAggregatedData()
    {
        var newsApiTask = _newsApiService.GetData(query: string.Empty);
        var openWeatherMapApiTask = _openWeatherMapApiService.GetData(query: string.Empty);
        var restCountriesApiTask = _restCountriesApiService.GetData(query: string.Empty);

        await Task.WhenAll(newsApiTask, openWeatherMapApiTask, restCountriesApiTask);

        var response = new AggregatedApiResponse()
        {
            NewsApiResponse = newsApiTask.IsCompletedSuccessfully ? newsApiTask.Result : new(),
            OpenWeatherMapApiResponse = openWeatherMapApiTask.IsCompletedSuccessfully ? openWeatherMapApiTask.Result : new(),
            RestCountriesApiResponse = restCountriesApiTask.IsCompletedSuccessfully ? restCountriesApiTask.Result : new(),
        };

        return response;
    }
}