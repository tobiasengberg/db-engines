using Cassandra;
using cassandra.Models;
using cassandra.Repositories;
using Microsoft.Extensions.Options;
using ISession = Cassandra.ISession;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CassandraSettings>(builder.Configuration.GetSection("CassandraSettings"));
builder.Services.AddSingleton<ICluster>(sp =>
{
    var cassandraSettings = sp.GetRequiredService<IOptions<CassandraSettings>>().Value;
    return Cluster.Builder()
        .AddContactPoints(cassandraSettings.ContactPoints)
        .WithPort(cassandraSettings.Port)
        .WithCredentials(cassandraSettings.Username, cassandraSettings.Password)
        .Build();
});
builder.Services.AddScoped<ISession>(sp =>
{
    var cluster = sp.GetRequiredService<ICluster>();
    var cassandraSettings = sp.GetRequiredService<IOptions<CassandraSettings>>().Value;
    return cluster.Connect(cassandraSettings.Keyspace);
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Add services to the container.
builder.Services.AddControllers();
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
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}