using Microsoft.EntityFrameworkCore;
using WeatherCollector.API.Data;
using WeatherCollector.API.Repositories;
using WeatherCollector.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlite("Data Source=weather.db"));

// Add HttpClient
builder.Services.AddHttpClient<WeatherService>();

// Add Repository and Service
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

var app = builder.Build();

// Auto create database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();