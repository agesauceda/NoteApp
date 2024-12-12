namespace NoteApp.Models.PhotoPage
{
    public class ImgNotePUT
    {
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public List<ImgPOST> fotos { get; set; }
    }
}
