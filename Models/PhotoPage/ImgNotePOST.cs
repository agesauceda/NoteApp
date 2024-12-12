using System.Text.Json.Serialization;

namespace NoteApp.Models.PhotoPage
{
    public class ImgNotePOST
    {
        [JsonPropertyName("ID")]
        public int? id { get; set; }
    }
}
