namespace WeatherCollector.API.Models;

public class WeatherApiResponse
{
    public WeatherMain Main { get; set; } = new();
    public WeatherWind Wind { get; set; } = new();
    public List<WeatherCondition> Weather { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public WeatherSys Sys { get; set; } = new();
}

public class WeatherMain
{
    public double Temp { get; set; }
    public double Feels_Like { get; set; }
    public double Humidity { get; set; }
}

public class WeatherWind
{
    public double Speed { get; set; }
}

public class WeatherCondition
{
    public string Description { get; set; } = string.Empty;
}

public class WeatherSys
{
    public string Country { get; set; } = string.Empty;
}