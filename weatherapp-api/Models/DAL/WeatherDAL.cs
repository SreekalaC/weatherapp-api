using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.CircuitBreaker;
using weatherapp_api.Interfaces.DAL;


namespace weatherapp_api.Models.DAL

{
    public class WeatherDAL : IWeatherDAL
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;
        public WeatherDAL(IHttpClientFactory httpClientFactory, IMemoryCache cache, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
            _configuration = configuration;
        }

        public async Task<WeatherData> GetCurrentWeatherByZipCode(string zipCode)
        {
            try
            {
                //if the weather data in cache will return that value.
                if (_cache.TryGetValue(zipCode, out string cachedWeatherData))
                {
                    return JsonSerializer.Deserialize<WeatherData>(cachedWeatherData);
                }
                else
                {
                    string apiKey = _configuration["WeatherAPI:Key"].ToString();
                    var httpClient = _httpClientFactory.CreateClient("WeatherAPI");
                    var response = await httpClient.GetAsync($"current.json?key={apiKey}&q={zipCode}&aqi=no");
                    var weatherData = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        _cache.Set(zipCode, weatherData, TimeSpan.FromMinutes(5));
                    }
                    var currentWeatherResult = JsonSerializer.Deserialize<WeatherData>(weatherData);
                    return currentWeatherResult;
                }
            }
            catch(BrokenCircuitException bce)
            {
                WeatherData weatherData = new WeatherData();
                weatherData.Error.Message = "Service is not available Please try again later";
                return weatherData;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error connecting to WeatherAPI in  Data Access Layer: {ex.Message}", ex);
            }
        }
    }
}
