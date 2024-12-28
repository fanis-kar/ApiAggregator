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

    public async Task<AggregatedApiResponse> GetAggregatedData(string query1, string query2, string query3)
    {
        var newsApiTask = _newsApiService.GetData(query: query1);
        var openWeatherMapApiTask = _openWeatherMapApiService.GetData(query: query2);
        var restCountriesApiTask = _restCountriesApiService.GetData(query: query3);

        await Task.WhenAll(newsApiTask, openWeatherMapApiTask, restCountriesApiTask);

        var successfullyExecutedTasks = Convert.ToInt16(newsApiTask.IsCompletedSuccessfully) + Convert.ToInt16(openWeatherMapApiTask.IsCompletedSuccessfully) + Convert.ToInt16(restCountriesApiTask.IsCompletedSuccessfully);

        var response = new AggregatedApiResponse()
        {
            MainResponse = new MainResponse()
            {
                Message = $"{successfullyExecutedTasks}/3 tasks were successfully executed.",
                SuccessfullyExecutedTasks = successfullyExecutedTasks
            },
            NewsApiResponse = newsApiTask.IsCompletedSuccessfully ? newsApiTask.Result : new(),
            OpenWeatherMapApiResponse = openWeatherMapApiTask.IsCompletedSuccessfully ? openWeatherMapApiTask.Result : new(),
            RestCountriesApiResponse = restCountriesApiTask.IsCompletedSuccessfully ? restCountriesApiTask.Result : new(),
        };

        return response;
    }
}