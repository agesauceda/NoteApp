

using NoteApp.Models.VoicePage;

namespace NoteApp.Views.Interfaces
{
    public interface VoicePageViewInterface
    {
        Task RegisterNoteVoice(string msg);
        Task GetAudio (List<AudiosNoteGET> voices);
        Task UpdateNoteVoice (string msg); 
    }
}
