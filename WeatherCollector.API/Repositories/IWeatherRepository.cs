using WeatherCollector.API.Models;

namespace WeatherCollector.API.Repositories;

public interface IWeatherRepository
{
    Task SaveWeatherDataAsync(WeatherData data);
    Task<IEnumerable<WeatherData>> GetWeatherHistoryAsync(string city);
    Task<WeatherData?> GetLatestWeatherAsync(string city);
    Task<IEnumerable<WeatherData>> GetAllAsync();
}