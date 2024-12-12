using NoteApp.Models.EventPage;
using NoteApp.Services.EventPage;
using NoteApp.Views.Interfaces;


namespace NoteApp.Controllers.EventPage
{
    public class EventPageController : EventPageControllerInterface
    {
        private readonly HttpClient _client = App._client.getClient();
        private EventPageService _service;
        private EventPageViewInterface _view;
        public EventPageController(EventPageViewInterface view) { 
            this._service = new EventPageService(_client);
            this._view = view;
        }
        public async Task InsertReminder(ReminderPOST e)
        {
            ApiResponseReminder response = await _service.InsertReminder(e);

            Console.WriteLine("Insertando recordatorio:");
            Console.WriteLine($"Título: {e.titulo}");
            Console.WriteLine($"Descripción: {e.descripcion}");
            Console.WriteLine($"Ubicación: {e.ubicacion}");
            Console.WriteLine($"Fecha inicio: {e.fecha_inicio}");
            Console.WriteLine($"Fecha final: {e.fecha_final}");

            if (response.status.Value)
            {
                await _view.InsertReminder(response.message);
            }
            else
            {
                await _view.InsertReminder("Error al registrar el Evento");
            }
        }
    }
}