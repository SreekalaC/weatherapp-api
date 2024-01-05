using System;
using Polly;
using Polly.Extensions.Http;
using weatherapp_api.Interfaces.BLL;
using weatherapp_api.Interfaces.DAL;
using weatherapp_api.Models.BLL;
using weatherapp_api.Models.DAL;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHttpClient("WeatherAPI", c =>
{
    c.BaseAddress = new Uri("http://api.weatherapi.com/v1/");
})
.AddPolicyHandler(GetRetryPolicy())
.AddPolicyHandler(GetCircuitBreakerPolicy());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IWeatherBLL, WeatherBLL>();
builder.Services.AddTransient<IWeatherDAL, WeatherDAL>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:8080")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configure JSON serialization options
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new SystemTextJsonActionConverter());
});
builder.Services.AddMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(1, TimeSpan.FromSeconds(20));
}
IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(1, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)));
}

public class SystemTextJsonActionConverter : JsonConverter<Action>
{
    public override Action Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Implement custom deserialization logic if needed
        throw new NotImplementedException("Deserialization of System.Action is not supported.");
    }

    public override void Write(Utf8JsonWriter writer, Action value, JsonSerializerOptions options)
    {
    }
}