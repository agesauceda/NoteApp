using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteApp.Models.VoicePage
{
    public class ApiResponseNotesVoicePOST
    {
        [JsonPropertyName("status")]
        public bool? status { get; set; }
        [JsonPropertyName("statusCode")]
        public int? statusCode { get; set; }
        [JsonPropertyName("message")]
        public string? message { get; set; }
        [JsonPropertyName("data")]
        public AudiosNoteGET data { get; set; }
        [JsonPropertyName("isLogin")]
        public bool? isLogin { get; set; }
        [JsonPropertyName("timestamp")]
        public string? timestamp { get; set; }
    }
}
