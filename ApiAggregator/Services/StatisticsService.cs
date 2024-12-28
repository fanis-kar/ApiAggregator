using ApiAggregator.Services.Interfaces;

namespace ApiAggregator.Services;

public class StatisticsService : IStatisticsService
{
    private readonly object _lock = new();
    private readonly Dictionary<string, List<long>> _apiStatistics = new();

    public void LogRequest(string apiName, long responseTime)
    {
        lock (_lock)
        {
            if (!_apiStatistics.ContainsKey(apiName))
            {
                _apiStatistics[apiName] = new List<long>();
            }
            _apiStatistics[apiName].Add(responseTime);
        }
    }

    public Dictionary<string, object> GetStatistics()
    {
        var statistics = new Dictionary<string, object>();

        lock (_lock)
        {
            foreach (var entry in _apiStatistics)
            {
                var apiName = entry.Key;
                var responseTimes = entry.Value;

                statistics[apiName] = new
                {
                    TotalRequests = responseTimes.Count,
                    AverageResponseTime = responseTimes.Average(),
                    PerformanceBuckets = new
                    {
                        Fast = responseTimes.Count(x => x < 100),
                        Average = responseTimes.Count(x => x >= 100 && x <= 200),
                        Slow = responseTimes.Count(x => x > 200)
                    }
                };
            }
        }

        return statistics;
    }
}