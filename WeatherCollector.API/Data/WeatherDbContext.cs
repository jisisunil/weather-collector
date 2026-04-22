using Microsoft.EntityFrameworkCore;
using WeatherCollector.API.Models;

namespace WeatherCollector.API.Data;

public class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) 
        : base(options)
    {
    }

    public DbSet<WeatherData> WeatherRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherData>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.City).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Country).IsRequired().HasMaxLength(10);
            entity.Property(e => e.Temperature).IsRequired();
            entity.Property(e => e.RecordedAt).IsRequired();
        });
    }
}