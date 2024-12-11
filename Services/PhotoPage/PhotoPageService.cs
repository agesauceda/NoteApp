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
    }
}
