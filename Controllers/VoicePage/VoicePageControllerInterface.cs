
namespace NoteApp.Controllers.VoicePage
{
    public interface VoicePageControllerInterface
    {
        Task RegisterNoteVoice(Models.VoicePage.NoteVoicePOST e);
        Task GetAudios(int id);
        Task UpdateNoteVoice(Models.VoicePage.AudioNotePUT e, int id);
    }
}
