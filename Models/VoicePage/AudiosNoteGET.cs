using System.Text.Json.Serialization;

namespace NoteApp.Models.VoicePage
{
    public class AudiosNoteGET
    {
        [JsonPropertyName("id")]
        public int? id { get; set; }
        [JsonPropertyName("base64")]
        public string? base64 { get; set; }
    }
}