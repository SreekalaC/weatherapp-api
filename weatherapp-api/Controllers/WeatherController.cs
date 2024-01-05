using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.CircuitBreaker;
using weatherapp_api.Interfaces.BLL;
using weatherapp_api.Models;

namespace weatherapp_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherBLL _weatherBLL;

        public WeatherController( IWeatherBLL weatherBLL )
        {
            _weatherBLL = weatherBLL;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentWeatherByZipcode(string zipCode)
        {
            try
            {
                var currentWeather = await  _weatherBLL.GetCurrentWeatherByZipCode(zipCode);
                if (currentWeather != null)
                {
                    return Ok(currentWeather);
                }
                else
                {
                    return (NotFound());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
