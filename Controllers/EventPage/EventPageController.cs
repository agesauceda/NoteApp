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

            string datos = response.data != null ? string.Join(", ", response.data.Select(r => r.ToString())) : "Sin datos";
            await App.Current.MainPage.DisplayAlert(
                "Respuesta del Servidor",
                $"Estado: {(response.status.HasValue ? response.status.Value.ToString() : "Nulo")}\n" +
                $"Mensaje: {response.message}\n" +
                $"Datos: {datos}",
                "OK"
            );

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