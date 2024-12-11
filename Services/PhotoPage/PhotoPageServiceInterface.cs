using NoteApp.Models.PhotoPage;

namespace NoteApp.Services.PhotoPage
{
    public interface PhotoPageServiceInterface
    {
        Task<ApiResponseNoteImgPOST> InsertNoteImg(NotesImgPOST e);
    }
}
