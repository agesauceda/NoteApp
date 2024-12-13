using System.Text.Json.Serialization;

namespace NoteApp.Models.EventPage
{
    public class ReminderGET
    {
        [JsonPropertyName("id")]
        public int? id { get; set; }
        [JsonPropertyName("titulo")]
        public string? titulo { get; set; }
        [JsonPropertyName("descripcion")]
        public string? descripcion { get; set; }
        [JsonPropertyName("ubicacion")]
        public string? ubicacion { get; set; }
        [JsonPropertyName("imagen")]
        public string? imagen { get; set; }
        [JsonPropertyName("fechaInicio")]
        public string? fecha_inicio { get; set; }
        [JsonPropertyName("fechaFinal")]
        public string? fecha_final { get; set; }
        
    }
}
