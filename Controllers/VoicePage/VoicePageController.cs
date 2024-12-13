
using NoteApp.Models.VoicePage;
using NoteApp.Services.VoicePage;
using NoteApp.Views.Interfaces;

namespace NoteApp.Controllers.VoicePage
{
    public class VoicePageController : VoicePageControllerInterface
    {
        private readonly HttpClient _client = App._client.getClient();
        private VoicePageService _service;
        private VoicePageViewInterface _view;

        public VoicePageController(VoicePageViewInterface view)
        {
            this._service = new VoicePageService(_client);
            this._view = view;
        }


        public async Task RegisterNoteVoice(NoteVoicePOST e)
        {
            ApiResponseNotesVoice response = await _service.RegisterNoteVoice(e);
            if (response.status.Value) 
            { 
                await _view.RegisterNoteVoice(response.message);
            }
            else
            {
                await _view.RegisterNoteVoice("Error al guardar nota.");
            }
        }
    }
}
