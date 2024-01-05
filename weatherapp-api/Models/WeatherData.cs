using System.Text.Json.Serialization;

namespace weatherapp_api.Models
{
    public class WeatherData
    {
        //gets or sets location
        [JsonPropertyName("location")]
        public Location? Location { get; set; }

        //gets or sets current weather
        [JsonPropertyName("current")]
        public CurrentWeather? CurrentWeather { get; set; }

        //gets or sets error
        [JsonPropertyName("error")]
        public ErrorDetails? Error { get; set; }

    }
}
