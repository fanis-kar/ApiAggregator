using ApiAggregator.Models.ExternalServices;

namespace ApiAggregator.UnitTests;

public static class PrepareData
{
    public static NewsApiResponse GetNewsApiData()
    {
        var fakeNewsApiResponse = new NewsApiResponse
        {
            Status = "ok",
            TotalResults = 2,
            Articles =
            [
                new NewsApiResponse.Article
                {
                    Source = new NewsApiResponse.Source
                    {
                        Id = null,
                        Name = "Yahoo Entertainment"
                    },
                    Author = "Steve Dent",
                    Title = "Humane CosmOS kinda turns everything into a Pin",
                    Description = "Its universally derided AI pin was a flop, so Humane now pivoting to software...",
                    Url = "https://consent.yahoo.com/v2/collectConsent?sessionId=1_cc-session_c69e4096-cd6b-41f5-8f6e-459df4ea2562",
                    UrlToImage = null,
                    PublishedAt = new DateTime(2024, 12, 5, 13, 30, 18, DateTimeKind.Utc),
                    Content = "If you click 'Accept all'..."
                },
                new NewsApiResponse.Article
                {
                    Source = new NewsApiResponse.Source
                    {
                        Id = "wired",
                        Name = "Wired"
                    },
                    Author = "Jeremy White",
                    Title = "The Electric Explorer’s Nightmare Launch...",
                    Description = "Plagued by delays, tied to a rival’s electric platform...",
                    Url = "https://www.wired.com/story/ford-electric-explorer-ev-test-drive/",
                    UrlToImage = "https://media.wired.com/photos/6763277ec043a0dcb0432abc/191:100/w_1280,c_limit/ford-gear.jpg",
                    PublishedAt = new DateTime(2024, 12, 21, 14, 30, 0, DateTimeKind.Utc),
                    Content = "Then, in August..."
                }
            ]
        };

        return fakeNewsApiResponse;
    }

    public static OpenWeatherMapApiResponse GetOpenWeatherMapApiData()
    {
        return new OpenWeatherMapApiResponse
        {
            Coord = new Coord
            {
                Lat = 51.5085,
                Lon = -0.1257 // Longitude for London
            },
            Weather =
            [
                new() {
                    Id = 804,
                    Main = "Clouds",
                    Description = "overcast clouds",
                    Icon = "04n"
                }
            ],
            Base = "stations",
            Main = new Main
            {
                Temp = 278.2,
                TempMin = 277.6,
                TempMax = 279.14,
                Pressure = 1031,
                Humidity = 96,
            },
            Visibility = 6000,
            Wind = new Wind
            {
                Speed = 0.51,
                Deg = 0
            },
            Clouds = new Clouds
            {
                All = 100
            },
            Dt = 1735342913, // Timestamp in seconds
            Sys = new Sys
            {
                Type = 2,
                Id = 2091269,
                Country = "GB",
                Sunrise = 1735286759, // Timestamp for sunrise
                Sunset = 1735315033 // Timestamp for sunset
            },
            Timezone = 0,
            Id = 2643743, // London ID from OpenWeatherMap
            Name = "London",
            Cod = 200 // OK status
        };
    }

    public static RestCountriesApiResponse GetRestCountriesApiData()
    {
        return new RestCountriesApiResponse
        {
            Countries =
            [
                new Country
                {
                    Name = new Name
                    {
                        Common = "French Guiana",
                        Official = "Guiana"
                    },
                    Cca2 = "GF",
                    Ccn3 = "254",
                    Cca3 = "GUF",
                    Cioc = "GUF",
                    Flags = new Flags
                    {
                        Png = "https://flagcdn.com/w320/gf.png",
                        Svg = "https://flagcdn.com/gf.svg"
                    },
                    Capital = ["Cayenne"],
                    Region = "Americas",
                    Subregion = "South America",
                    Population = 254541,
                    Languages = new Dictionary<string, string>
                    {
                        { "fra", "French" }
                    },
                    Currencies = new Dictionary<string, Currency>
                    {
                        { "EUR", new Currency { Name = "Euro", Symbol = "€" } }
                    }
                },
                new Country
                {
                    Name = new Name
                    {
                        Common = "Kosovo",
                        Official = "Republic of Kosovo"
                    },
                    Cca2 = "XK",
                    Ccn3 = "UNKNOWN",
                    Cca3 = "UNK",
                    Cioc = "KOS",
                    Flags = new Flags
                    {
                        Png = "https://flagcdn.com/w320/xk.png",
                        Svg = "https://flagcdn.com/xk.svg"
                    },
                    Capital = ["Pristina"],
                    Region = "Europe",
                    Subregion = "Southeast Europe",
                    Population = 1775378,
                    Languages = new Dictionary<string, string>
                    {
                        { "sqi", "Albanian" },
                        { "srp", "Serbian" }
                    },
                    Currencies = new Dictionary<string, Currency>
                    {
                        { "EUR", new Currency { Name = "Euro", Symbol = "€" } }
                    }
                }
            ]
        };
    }

    public static AggregatedApiResponse GetAggregatedApiData()
    {
        return new AggregatedApiResponse
        {
            NewsApiResponse = GetNewsApiData(),
            OpenWeatherMapApiResponse = GetOpenWeatherMapApiData(),
            RestCountriesApiResponse = GetRestCountriesApiData()
        };
    }
}