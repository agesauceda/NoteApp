using System.Text.Json.Serialization;

namespace NoteApp.Models.Dashboard
{
    public class ObjectDashBoard
    {
        [JsonPropertyName("ID")]
        public int? id { get; set; }
        [JsonPropertyName("TITULO")]
        public string? titulo { get; set; }
        [JsonPropertyName("CONTENIDO")]
        public string? contenido { get; set; }
        [JsonPropertyName("FECHA INICIO")]
        public string? fechaInicio { get; set; }
        [JsonPropertyName("FECHA FINAL")]
        public string? fechaFinal { get; set; }
        [JsonPropertyName("TIPO NOTA")]
        public string? tipoNota { get; set; }
        [JsonPropertyName("FECHA CREACION")]
        public string? fechaCreacion { get; set; }
    }
}
