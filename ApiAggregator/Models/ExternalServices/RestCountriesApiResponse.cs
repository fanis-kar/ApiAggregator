namespace ApiAggregator.Models.ExternalServices;

public class RestCountriesApiResponse
{
    public List<Country> Countries { get; set; }
}

public class Country
{
    public Name Name { get; set; }
    public string Cca2 { get; set; }
    public string Ccn3 { get; set; }
    public string Cca3 { get; set; }
    public string Cioc { get; set; }
    public Flags Flags { get; set; }
    public List<string> Capital { get; set; }
    public string Region { get; set; }
    public string Subregion { get; set; }
    public long Population { get; set; }
    public Dictionary<string, string> Languages { get; set; }
    public Dictionary<string, Currency> Currencies { get; set; }
}

public class Name
{
    public string Common { get; set; }
    public string Official { get; set; }
}

public class Flags
{
    public string Png { get; set; }
    public string Svg { get; set; }
}

public class Currency
{
    public string Name { get; set; }
    public string Symbol { get; set; }
}
