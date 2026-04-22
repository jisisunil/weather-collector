using Microsoft.AspNetCore.Mvc;
using WeatherCollector.API.Services;

namespace WeatherCollector.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly ILogger<WeatherController> _logger;

    public WeatherController(IWeatherService weatherService, ILogger<WeatherController> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        try
        {
            var weather = await _weatherService.GetAndSaveWeatherAsync(city);
            return Ok(weather);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather for {City}", city);
            return StatusCode(500, $"Error fetching weather: {ex.Message}");
        }
    }

    [HttpGet("{city}/history")]
    public async Task<IActionResult> GetHistory(string city)
    {
        var history = await _weatherService.GetWeatherHistoryAsync(city);
        return Ok(history);
    }

    [HttpGet("{city}/latest")]
    public async Task<IActionResult> GetLatest(string city)
    {
        var latest = await _weatherService.GetLatestWeatherAsync(city);
        if (latest == null) return NotFound($"No data found for {city}");
        return Ok(latest);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var all = await _weatherService.GetAllAsync();
        return Ok(all);
    }
}