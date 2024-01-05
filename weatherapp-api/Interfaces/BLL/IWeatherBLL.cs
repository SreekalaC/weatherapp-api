using weatherapp_api.Models;

namespace weatherapp_api.Interfaces.BLL
{
    public interface IWeatherBLL
    {
        Task<WeatherData> GetCurrentWeatherByZipCode(string zipCode);
    }
}
