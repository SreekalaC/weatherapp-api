using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;
using weatherapp_api.Interfaces.BLL;
using weatherapp_api.Interfaces.DAL;

namespace weatherapp_api.Models.BLL
{
    public class WeatherBLL : IWeatherBLL
    {
        public readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;
        public readonly IWeatherDAL _weatherDAL;

        public WeatherBLL(IWeatherDAL weatherDAL)
        {
            _weatherDAL = weatherDAL;
        }
        public async Task<WeatherData> GetCurrentWeatherByZipCode(string zipCode)
        {
            try
            {
                var weatherData = await _weatherDAL.GetCurrentWeatherByZipCode(zipCode);
                return weatherData;
            }
            catch (BrokenCircuitException bce)
            {
                throw new Exception("WeatherAPI circuit is open in Business Layer. Please try again later.", bce);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting weather data in Business Layer: {ex.Message}", ex);
            }
        }
    }
}
