using NoteApp.Models.common;
using NoteApp.Models.TextPage;
using System.Net.Http.Json;
using System.Text;
namespace NoteApp.Services.NotePage
{

    public class TextPageService : TextPageServiceInterface
    {
        private readonly HttpClient _client;

        // Constructor donde se pasa el HttpClient
        public TextPageService(HttpClient client)
        {
            _client = client;
        }

        //public Task<ApiResponse> CreateNote(NoteTextPOST e)
        //{
        //    throw new NotImplementedException();
        //}

        // Crear una nueva nota
        public async Task<ApiResponse> CreateNote(NoteTextPOST? note)
        {
            using (HttpResponseMessage response = await _client.PostAsJsonAsync("notasTexto/", note))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    if (content != null)
                    {
                        return content;
                    }
                }
                return new ApiResponse { status = false, message = "Error al crear la nota" };
            }
        }
    }
}
