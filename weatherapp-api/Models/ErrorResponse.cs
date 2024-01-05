using System.Text.Json.Serialization;

namespace weatherapp_api.Models
{
    public class ErrorResponse
    {
        //gets or set Error
        [JsonPropertyName("error")]
        public ErrorDetails Error { get; set; }
    }

    public class ErrorDetails
    {
        //gets or sets code
        [JsonPropertyName("code")]
        public int Code { get; set; }

        //gets or sets Message error
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
