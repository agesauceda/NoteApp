
namespace NoteApp.Services.VoicePage
{
    public interface VoicePageServiceInterface
    {
        Task<Models.VoicePage.ApiResponseNotesVoice> RegisterNoteVoice(Models.VoicePage.NoteVoicePOST e);
    }
}
