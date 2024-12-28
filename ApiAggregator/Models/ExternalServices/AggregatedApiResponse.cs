namespace ApiAggregator.Models.ExternalServices;

public class AggregatedApiResponse
{
    public NewsApiResponse NewsApiResponse { get; set; }
    public OpenWeatherMapApiResponse OpenWeatherMapApiResponse { get; set; }
    public RestCountriesApiResponse RestCountriesApiResponse { get; set; }
}