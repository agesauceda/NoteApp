
using NoteApp.Models.common;
using NoteApp.Models.VoicePage;
using System.Linq.Expressions;
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

        public async Task<ApiResponseNotesVoice> GetAudios(int id)
        {
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponseNotesVoice>($"notasVoz/{id}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new ApiResponseNotesVoice()
            {
                status = false
            };
        }

        public async Task<ApiResponseNotesVoicePOST> RegisterNoteVoice(NoteVoicePOST? e)
        {
            ApiResponseNotesVoicePOST response = null;
            try
            {
                using HttpResponseMessage res = await _client.PostAsJsonAsync("notasVoz/", e);
                if (res.IsSuccessStatusCode)
                {
                    var content = await res.Content.ReadFromJsonAsync<ApiResponseNotesVoicePOST>();
                    response = content;
                }

            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message); 

            }
            return response;
        }

        public async Task<ApiResponse> UpdateNoteVoice(AudioNotePUT e, int id)
        {
            ApiResponse response = null;
            try
            {
                using HttpResponseMessage res = await _client.PutAsJsonAsync($"notasVoz/{id}", e);
                if (res.IsSuccessStatusCode)
                {
                    var content = await res.Content.ReadFromJsonAsync<ApiResponse>();
                    response = content;
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
            return response;
        }
    }
}
