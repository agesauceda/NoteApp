using System.Text.Json.Serialization;

namespace NoteApp.Models.Dashboard
{
    public class ApiResponseDashboard
    {
        [JsonPropertyName("status")]
        public bool? status { get; set; }
        [JsonPropertyName("statusCode")]
        public int? statusCode { get; set; }
        [JsonPropertyName("message")]
        public string? message { get; set; }
        [JsonPropertyName("data")]
        public ObjectDashBoard[]? data { get; set; }
        [JsonPropertyName("isLogin")]
        public bool? isLogin { get; set; }
        [JsonPropertyName("timestamp")]
        public string? timestamp { get; set; }
    }
}
