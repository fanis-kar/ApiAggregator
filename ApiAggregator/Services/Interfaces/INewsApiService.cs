using ApiAggregator.Models.ExternalServices;

namespace ApiAggregator.Services.Interfaces;

public interface INewsApiService
{
    Task<NewsApiResponse> GetData(string query);
}