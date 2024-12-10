using System.Text.Json.Serialization;

namespace NoteApp.Models.PhotoPage
{
    public class ApiResponseNoteImg
    {
        [JsonPropertyName("status")]
        public bool? status { get; set; }
        [JsonPropertyName("statusCode")]
        public int? statusCode { get; set; }
        [JsonPropertyName("message")]
        public string? message { get; set; }
        [JsonPropertyName("data")]
        public ImgNoteGET[]? data { get; set; }
        [JsonPropertyName("isLogin")]
        public bool? isLogin { get; set; }
        [JsonPropertyName("timestamp")]
        public string? timestamp { get; set; }
    }
}
