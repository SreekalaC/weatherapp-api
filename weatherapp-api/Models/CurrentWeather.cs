using System.Text.Json.Serialization;

namespace weatherapp_api.Models
{
    public class CurrentWeather
    {
        //gets or sets Last Updated ones
        [JsonPropertyName("last_updated_epoch")]
        public long LastUpdatedEpoch { get; set; }

        //gets or sets last updated weather
        [JsonPropertyName("last_updated")]
        public string? LastUpdated { get; set; }

        //gets or sets temparture in celcius
        [JsonPropertyName("temp_c")]
        public double TemperatureCelsius { get; set; }

        //gets or sets temprature in farenheit
        [JsonPropertyName("temp_f")]
        public double TemperatureFahrenheit { get; set; }

        //gets or sets IsDay
        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }

        //gets or sets windMPh
        [JsonPropertyName("wind_mph")]
        public double WindMph { get; set; }

        //gets or sets windKPh
        [JsonPropertyName("wind_kph")]
        public double WindKph { get; set; }

        //gets or sets condition
        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }

    }
}
