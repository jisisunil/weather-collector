using System.Text.Json;
using WeatherCollector.API.Models;
using WeatherCollector.API.Repositories;

namespace WeatherCollector.API.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _repository;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(
        IWeatherRepository repository,
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<WeatherService> logger)
    {
        _repository = repository;
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<WeatherData> GetAndSaveWeatherAsync(string city)
    {
        var apiKey = _configuration["OpenWeatherMap:ApiKey"];
        var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

        _logger.LogInformation("Fetching weather for {City}", city);

        var response = await _httpClient.GetStringAsync(url);
        
        var options = new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true 
        };
        
        var apiResponse = JsonSerializer.Deserialize<WeatherApiResponse>(response, options)!;

        var weatherData = new WeatherData
        {
            City = city,
            Country = apiResponse.Sys.Country,
            Temperature = apiResponse.Main.Temp,
            FeelsLike = apiResponse.Main.Feels_Like,
            Humidity = apiResponse.Main.Humidity,
            WindSpeed = apiResponse.Wind.Speed,
            Description = apiResponse.Weather.FirstOrDefault()?.Description ?? "",
            RecordedAt = DateTime.UtcNow
        };

        await _repository.SaveWeatherDataAsync(weatherData);
        
        return weatherData;
    }

    public async Task<IEnumerable<WeatherData>> GetWeatherHistoryAsync(string city)
        => await _repository.GetWeatherHistoryAsync(city);

    public async Task<WeatherData?> GetLatestWeatherAsync(string city)
        => await _repository.GetLatestWeatherAsync(city);

    public async Task<IEnumerable<WeatherData>> GetAllAsync()
        => await _repository.GetAllAsync();
}