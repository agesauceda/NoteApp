using System.Text.Json.Serialization;

namespace NoteApp.Models.common.ResponseMailer
{
    public class Data
    {
        [JsonPropertyName("msg")]
        public List<String>? Msg { get; set; }
        [JsonPropertyName("content")]
        public Content? Content { get; set; }
    }
}
