using NoteApp.Models.PhotoPage;

namespace NoteApp.Controllers.PhotoPage
{
    public interface PhotoPageControllerInterface
    {
        Task insertNotePhoto(NotesImgPOST e);
    }
}
