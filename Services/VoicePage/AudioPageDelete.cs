using NoteApp.Models.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Services.VoicePage
{
    public class AudioPageDelete
    {
        private readonly HttpClient _client = App._client.getClient();

        public async Task<ApiResponse> DeleteNoteAudio(int id)
        {
            ApiResponse response = null;
            try
            {
                using HttpResponseMessage res = await _client.DeleteAsync($"notasVoz/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var data = await res.Content.ReadFromJsonAsync<ApiResponse>();
                    response = data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse> DeleteVoice(int id)
        {
            ApiResponse response = null;
            try
            {
                using HttpResponseMessage res = await _client.DeleteAsync($"notasVoz/audio/{id}");
                if (res.IsSuccessStatusCode)
                {
                    var data = await res.Content.ReadFromJsonAsync<ApiResponse>();
                    response = data;
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
