namespace ApiAggregator.Services.Interfaces;

public interface IStatisticsService
{
    void LogRequest(string apiName, long responseTime);
    Dictionary<string, object> GetStatistics();
}
