
using NoteApp.Models.common;
using NoteApp.Models.VoicePage;

namespace NoteApp.Services.VoicePage
{
    public interface VoicePageServiceInterface
    {
        Task<Models.VoicePage.ApiResponseNotesVoicePOST> RegisterNoteVoice(Models.VoicePage.NoteVoicePOST e);
        Task<Models.VoicePage.ApiResponseNotesVoice> GetAudios(int id);
        Task <ApiResponse> UpdateNoteVoice(AudioNotePUT e, int id);


    }
}
