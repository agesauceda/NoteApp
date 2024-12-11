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

        // Cargar los datos por Id
        public async Task<NoteTextPOST?> GetNoteForEdit(int id)
        {
            using (HttpResponseMessage response = await _client.GetAsync($"notasTexto/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var note = await response.Content.ReadFromJsonAsync<NoteTextPOST>();
                    return note;
                }
                return null; // si la nota es null
            }
        }

        // Actualizar una nota
        public async Task<ApiResponse> UpdateNote(int id, NoteTextPOST? note)
        {
            using (HttpResponseMessage response = await _client.PutAsJsonAsync($"notasTexto/{id}", note))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<ApiResponse>();
                    if (content != null)
                    {
                        return content;
                    }
                }
                return new ApiResponse { status = false, message = "Error al actualizar la nota" };
            }
        }
    }
}
