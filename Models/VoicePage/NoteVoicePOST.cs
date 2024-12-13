namespace NoteApp.Models.VoicePage
{
    public class NoteVoicePOST
    {
        public string? titulo { get; set; }
        public string descripcion { get; set; }
        public List<string> audio { get; set; }
    }
}
