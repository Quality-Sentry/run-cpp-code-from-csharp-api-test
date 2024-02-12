using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/GetRandomNumber", () =>
{
    var filename = "random_number.exe";
    var process = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = filename,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        }
    };

    process.Start();
    process.WaitForExit();
    var output = process.StandardOutput.ReadToEnd();

    // Try to parse the output as an integer
    if (int.TryParse(output, out var result))
    {
        return result;
    }

    return 0;
});



app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

