using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NoteApp.Models.VoicePage
{
    public class AudioNotePOST
    {
        [JsonPropertyName("ID")]
        public int? id { get; set; }
    }
}
