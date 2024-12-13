
using NoteApp.Models.common;
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

        public async Task GetAudios(int id)
        {
            ApiResponseNotesVoice response = await _service.GetAudios(id);
            if (response.status.Value)
            {
                var voices = response.data.ToList();
                await _view.GetAudio(voices);
            }
            else
            {
                await _view.GetAudio(null);
            }
        }

        public async Task RegisterNoteVoice(NoteVoicePOST e)
        {
            ApiResponseNotesVoicePOST response = await _service.RegisterNoteVoice(e);
            if (response.status.Value) 
            { 
                await _view.RegisterNoteVoice(response.message);
            }
            else
            {
                await _view.RegisterNoteVoice("Error al guardar nota.");
            }
        }

        public async Task UpdateNoteVoice(AudioNotePUT e, int id)
        {
            try
            {
                ApiResponse response = await _service.UpdateNoteVoice(e, id);
                if (response.status.Value)
                {
                    await _view.UpdateNoteVoice(response.message);
                }
                else
                {
                    await _view.UpdateNoteVoice("Error al actualizar la nota");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
