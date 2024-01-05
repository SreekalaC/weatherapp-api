using weatherapp_api.Models;

namespace weatherapp_api.Interfaces.DAL
{
    public interface IWeatherDAL
    {
       Task<WeatherData> GetCurrentWeatherByZipCode(string zipCode);
    }
}
