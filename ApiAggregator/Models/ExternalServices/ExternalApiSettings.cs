namespace ApiAggregator.Models.ExternalServices;

public class ExternalApiSettings
{
    public NewsApiSettings NewsApi { get; set; }
    public OpenWeatherMapApiSettings OpenWeatherMapApi { get; set; }
    public RestCountriesApiSettings RestCountriesApi { get; set; }
}

public abstract class ApiSettingsBase
{
    public string Url { get; set; }
    public string ApiKey { get; set; }
}

public class NewsApiSettings : ApiSettingsBase
{
}

public class OpenWeatherMapApiSettings : ApiSettingsBase
{
}

public class RestCountriesApiSettings : ApiSettingsBase
{
}