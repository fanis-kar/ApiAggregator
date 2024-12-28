using ApiAggregator.Models.ExternalServices;

namespace ApiAggregator.Services.Interfaces;

public interface IApiAggregationService
{
    Task<AggregatedApiResponse> GetAggregatedData(string query1, string query2, string query3);
}