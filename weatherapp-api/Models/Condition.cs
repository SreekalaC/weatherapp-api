using System.Text.Json.Serialization;

namespace weatherapp_api.Models
{
    public class Condition
    {
        //gets or sets Text
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        //gets or sets Icon for weather
        [JsonPropertyName("icon")]
        public string? Icon { get; set; }

        //gets or sets Code
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
