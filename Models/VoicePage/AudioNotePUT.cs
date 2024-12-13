using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Models.VoicePage
{
    public class AudioNotePUT
    {
        public string titulo { get; set; }
        public string descripcion { get; set; } 
        public List<AudioPOST> audio { get; set; }
    }
}
