using System.Text.Json.Serialization;
namespace NoteApp.Models.common.ResponseMailer
{
    public class Content
    {
        [JsonPropertyName("status")]
        public bool? Status { get; set; }
    }
}
