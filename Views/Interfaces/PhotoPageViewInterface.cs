using NoteApp.Models.PhotoPage;

namespace NoteApp.Views.Interfaces
{
    public interface PhotoPageViewInterface
    {
        Task insertNotePhoto(string msg);
        Task getImages(List<ImgNoteGET> imgs);
        Task updateNotePhoto(string msg);
    }
}
