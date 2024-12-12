using NoteApp.Models.common;
using NoteApp.Models.EventPage;
using System.Net.Http.Json;

namespace NoteApp.Services.EventPage
{
    public class EventPageService : EventPageServiceInterface
    {
        private readonly HttpClient _client;
        public EventPageService(HttpClient client) {
            _client = client;
        }
        
        public async Task<ApiResponseReminder> InsertReminder(ReminderPOST e)
        {
            using HttpResponseMessage response = await _client.PostAsJsonAsync("recordatorios/", e);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadFromJsonAsync<ApiResponseReminder>();
                if (content != null) {
                    return content;
                }
            }
            return new ApiResponseReminder { status = false, message = "Error al registrar usuario" };
        }
    }
}
