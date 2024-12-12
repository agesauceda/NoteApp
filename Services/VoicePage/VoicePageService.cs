
using NoteApp.Models.VoicePage;
using System.Net.Http.Json;

namespace NoteApp.Services.VoicePage
{
    public class VoicePageService : VoicePageServiceInterface
    {
        private readonly HttpClient _client;
        public VoicePageService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<ApiResponseNotesVoice> RegisterNoteVoice(NoteVoicePOST? e)
        {
            using HttpResponseMessage response = await _client.PostAsJsonAsync("notasVoz/", e);
            if (response.IsSuccessStatusCode) 
            {
                var content = await response.Content.ReadFromJsonAsync<ApiResponseNotesVoice>();
                if (content != null) { 
                    return content;
                }
            }
            return new ApiResponseNotesVoice { status = false, message = "Error al guardar nota" };
        }
    }
}
