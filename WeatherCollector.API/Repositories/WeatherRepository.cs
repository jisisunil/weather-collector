using Microsoft.EntityFrameworkCore;
using WeatherCollector.API.Data;
using WeatherCollector.API.Models;

namespace WeatherCollector.API.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly WeatherDbContext _context;

    public WeatherRepository(WeatherDbContext context)
    {
        _context = context;
    }

    public async Task SaveWeatherDataAsync(WeatherData data)
    {
        await _context.WeatherRecords.AddAsync(data);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<WeatherData>> GetWeatherHistoryAsync(string city)
    {
        return await _context.WeatherRecords
            .Where(w => w.City.ToLower() == city.ToLower())
            .OrderByDescending(w => w.RecordedAt)
            .Take(10)
            .ToListAsync();
    }

    public async Task<WeatherData?> GetLatestWeatherAsync(string city)
    {
        return await _context.WeatherRecords
            .Where(w => w.City.ToLower() == city.ToLower())
            .OrderByDescending(w => w.RecordedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<WeatherData>> GetAllAsync()
    {
        return await _context.WeatherRecords
            .OrderByDescending(w => w.RecordedAt)
            .Take(50)
            .ToListAsync();
    }
}