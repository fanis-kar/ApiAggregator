using ApiAggregator.Models.ExternalServices;

namespace ApiAggregator.Services.Interfaces;

public interface IRestCountriesApiService
{
    Task<RestCountriesApiResponse> GetData(string query);
}