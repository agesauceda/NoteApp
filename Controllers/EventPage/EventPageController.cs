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
        public EventPageController(EventPageViewInterface view)
        {
            this._service = new EventPageService(_client);
            this._view = view;
        }
        public async Task InsertReminder(ReminderPOST e)
        {
            if (e == null)
            {
                await _view.InsertReminder("El recordatorio no puede ser nulo.");
                return;
            }
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

        public async Task GetReminder(int id)
        {
            ApiResponseReminder response = await _service.GetReminder(id);


            if (response.status.Value)
            {

                if (response.data == null || !response.data.Any())
                {
                    await _view.GetReminder(null);
                }
                else
                {
                    var reminder = response.data.FirstOrDefault();
                    await _view.GetReminder(reminder);
                }
            }
            else
            {
                await _view.GetReminder(null);
            }
        }

        public async Task UpdateReminder(ReminderPUT e, int id)
        {
            ApiResponseReminder response = await _service.UpdateReminder(e, id);

            if (response.status.HasValue && response.status.Value)
            {
                await _view.UpdateReminder(e);
            }
            else
            {
                await _view.UpdateReminder(null); // Opcional, puedes manejar errores aquí si es necesario.
            }
        }

    }
}