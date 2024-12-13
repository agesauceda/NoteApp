using System.Text.Json.Serialization;

namespace NoteApp.Models.EventPage
{
    public class ReminderPUT
    {
        [JsonPropertyName("id")]
        public int? id { get; set; }
        public string? titulo { get; set; }
        public string? descripcion { get; set; }
        public string? ubicacion { get; set; }
        public string? imagen { get; set; }
        public string? fecha_inicio { get; set; }
        public string? fecha_final { get; set; }
    }
}
