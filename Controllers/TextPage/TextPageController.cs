using NoteApp.Models.common;
using NoteApp.Models.TextPage;
using NoteApp.Services.NotePage;
using NoteApp.Views.Interfaces;

namespace NoteApp.Controllers.NotePage
{
    public class TextPageController : TextPageControllerInterface
    {
        private readonly HttpClient _client = App._client.getClient(); 
        private readonly TextPageService _service;
        private readonly TextPageViewInterface _view;

        // Constructor que recibe la vista
        public TextPageController(TextPageViewInterface view)
        {
            _service = new TextPageService(_client); // Instancia del servicio
            _view = view; // Referencia de la vista
        }

        // Método para crear una nota
        public async Task CreateNote(NoteTextPOST e)
        {
            ApiResponse response = await _service.CreateNote(e);
            if (response.status == true)
            {
                await _view.CreateNote("Nota creada exitosamente");
            }
            else
            {
                await _view.CreateNote(response.message ?? "Error al crear la nota");
            }
        }
    }
}
