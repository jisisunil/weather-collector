using WeatherCollector.API.Models;

namespace WeatherCollector.API.Services;

public interface IWeatherService
{
    Task<WeatherData> GetAndSaveWeatherAsync(string city);
    Task<IEnumerable<WeatherData>> GetWeatherHistoryAsync(string city);
    Task<WeatherData?> GetLatestWeatherAsync(string city);
    Task<IEnumerable<WeatherData>> GetAllAsync();
}