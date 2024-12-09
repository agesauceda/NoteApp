using System.Text.Json.Serialization;
namespace NoteApp.Models.common.ResponseMailer
{
    public class ApiResponseMail
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("statusCode")]
        public int? StatusCode { get; set; }
        [JsonPropertyName("data")]
        public Data? Data { get; set; }
        [JsonPropertyName("timestamp")]
        public string? Timestamp { get; set; }
    }
}
