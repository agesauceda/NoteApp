using NoteApp.Models.common;
using System.Net.Http.Json;

namespace NoteApp.Services.PhotoPage
{
    public class PhotoPageDelete
    {
        private readonly HttpClient _client = App._client.getClient();

        public async Task<ApiResponse> DeleteNoteImg(int id) {
            ApiResponse response = null;
            try {
                using HttpResponseMessage res = await _client.DeleteAsync($"notasImg/{id}");
                if (res.IsSuccessStatusCode) { 
                    var data = await res.Content.ReadFromJsonAsync<ApiResponse>();
                    response = data;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse> DeleteImg(int id) {
            ApiResponse response = null;
            try
            {
                Console.WriteLine("ID que se pasa: " + id);
                using HttpResponseMessage res = await _client.DeleteAsync($"notasImg/img/{id}");
                if(res.IsSuccessStatusCode)
                {
                    var data = await res.Content.ReadFromJsonAsync<ApiResponse>();
                    response = data;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return response;
        }
    }
}
