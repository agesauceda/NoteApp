using NoteApp.Models.common;
using NoteApp.Models.EventPage;
using System.Net.Http.Json;

namespace NoteApp.Services.EventPage
{
    public class EventPageService : EventPageServiceInterface
    {
        private readonly HttpClient _client;
        public EventPageService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResponseReminder> GetReminder(int id)
        {
            using HttpResponseMessage response = await _client.GetAsync($"recordatorios/{id}");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadFromJsonAsync<ApiResponseReminder>();
                  if (content != null && content.status.HasValue && content.status.Value)
                {
                    return content;
                }
                else
                {
                    return new ApiResponseReminder { status = false, message = "Recordatorio no encontrado" };
                }
            }
            else
            {
                return new ApiResponseReminder { status = false, message = "Error al obtener el recordatorio" };
            }

        }

        public async Task<ApiResponseReminder> InsertReminder(ReminderPOST e)
        {
            using HttpResponseMessage response = await _client.PostAsJsonAsync("recordatorios/", e);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ApiResponseReminder>();
                if (content != null)
                {
                    return content;
                }
            }
            return new ApiResponseReminder { status = false, message = "Error al registrar " };
        }

        public async Task<ApiResponseReminder> UpdateReminder(ReminderPUT e, int id)
        {
            using HttpResponseMessage response = await _client.PutAsJsonAsync($"recordatorios/{id}", e);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<ApiResponseReminder>();
                if (content != null)
                {
                    return content;
                }
            }
            return new ApiResponseReminder { status = false, message = "Error al actualizar el recordatorio" };
        }

    }
}
