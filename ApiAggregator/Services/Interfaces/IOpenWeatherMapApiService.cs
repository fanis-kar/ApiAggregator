using ApiAggregator.Models.ExternalServices;

namespace ApiAggregator.Services.Interfaces;

public interface IOpenWeatherMapApiService
{
    Task<OpenWeatherMapApiResponse> GetData(string query);
}