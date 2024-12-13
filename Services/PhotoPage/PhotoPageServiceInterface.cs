using NoteApp.Models.common;
using NoteApp.Models.PhotoPage;

namespace NoteApp.Services.PhotoPage
{
    public interface PhotoPageServiceInterface
    {
        Task<ApiResponseNoteImgPOST> InsertNoteImg(NotesImgPOST e);
        Task<ApiResponseNoteImg> GetImages(int id);
        Task<ApiResponse> UpdateNoteImg(ImgNotePUT e, int id);
    }
}
