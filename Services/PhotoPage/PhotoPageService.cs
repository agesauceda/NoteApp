using NoteApp.Models.common;
using NoteApp.Models.PhotoPage;
using System.Net.Http.Json;

namespace NoteApp.Services.PhotoPage
{
    public class PhotoPageService : PhotoPageServiceInterface
    {
        private readonly HttpClient _client;
        public PhotoPageService(HttpClient client) {
            _client = client;
        }

        public async Task<ApiResponseNoteImg> GetImages(int id)
        {
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponseNoteImg>($"notasImg/{id}");
                return response;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return new ApiResponseNoteImg() { status = false };
        }

        public async Task<ApiResponseNoteImgPOST> InsertNoteImg(NotesImgPOST e)
        {
            ApiResponseNoteImgPOST response = null;
            try
            {
                using HttpResponseMessage res = await _client.PostAsJsonAsync("notasImg/", e);
                if (res.IsSuccessStatusCode) {
                    var content = await res.Content.ReadFromJsonAsync<ApiResponseNoteImgPOST>();
                    response = content;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return response;
        }

        public async Task<ApiResponse> UpdateNoteImg(ImgNotePUT e, int id)
        {
            ApiResponse response = null;
            try {
                using HttpResponseMessage res = await _client.PutAsJsonAsync($"notasImg/{id}", e);
                if (res.IsSuccessStatusCode) {
                    var content = await res.Content.ReadFromJsonAsync<ApiResponse>();
                    response = content;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return response;
        }
    }
}
