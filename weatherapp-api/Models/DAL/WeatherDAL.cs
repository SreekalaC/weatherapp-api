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
        
        public WeatherDAL(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<WeatherData> GetCurrentWeatherByZipCode(string zipCode)
        {
            try
            {

                if (_cache.TryGetValue(zipCode, out string cachedWeatherData))
                {
                    return JsonSerializer.Deserialize<WeatherData>(cachedWeatherData);
                   
                }
                else
                {
                    var httpClient = _httpClientFactory.CreateClient("WeatherAPI");
                    var response = await httpClient.GetAsync($"current.json?key=75f0707a95da4fb3beb133716240401&q={zipCode}&aqi=no");
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
                throw new Exception($"Circuit is Open in  Data Access Layer: {bce.Message}", bce);

            }
            //catch (HttpRequestException ex)
            //{
            //    throw new Exception($"Error connecting to WeatherAPI in  Data Access Layer: {ex.Message}", ex);
            //}
        }
    }
}
