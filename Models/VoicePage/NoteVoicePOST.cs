namespace NoteApp.Models.VoicePage
{
    public class NoteVoicePOST
    {
        public int? id { get; set; }
        public string? titulo { get; set; }
        public string descripcion { get; set; }
        public List<string>? audio { get; set; } = new List<string>();
    }
}
