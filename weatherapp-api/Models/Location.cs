using System.Text.Json.Serialization;
using weatherapp_api.Models;

namespace weatherapp_api.Models
{
    public class Location
    {
        //gets or sets name
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        //gets or sets region
        [JsonPropertyName("region")]
        public string? Region { get; set; }

        //gets or sets country
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        //gets or sets Latitude
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        //gets or sets Longitude
        [JsonPropertyName("lon")]
        public double Lon { get; set; }

        //gets or sets Timezone id
        [JsonPropertyName("tz_id")]
        public string? TimeZoneId { get; set; }

        //gets or sets LocalTimeEpoch
        [JsonPropertyName("localtime_epoch")]
        public long LocalTimeEpoch { get; set; }

        //gets or sets LocalTime
        [JsonPropertyName("localtime")]
        public string? LocalTime { get; set; }
    }
}


