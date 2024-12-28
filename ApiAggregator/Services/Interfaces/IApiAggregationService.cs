using ApiAggregator.Models.ExternalServices;

namespace ApiAggregator.Services.Interfaces;

public interface IApiAggregationService
{
    Task<AggregatedApiResponse> GetAggregatedData();
}