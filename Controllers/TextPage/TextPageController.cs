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

        public TextPageController(TextPageViewInterface view)
        {
            _service = new TextPageService(_client); 
            _view = view; 
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

        // Método para actualizar una nota
        public async Task UpdateNote(int id, NoteTextPOST e)
        {
            ApiResponse response = await _service.UpdateNote(id, e);
            if ((bool)response.status)
            {
                await _view.UpdateNote("Nota actualizada exitosamente");
            }
            else
            {
                await _view.UpdateNote(response.message ?? "Error al actualizar la nota");
            }
        }

        //// método para cargar los datos 
        //public async task<notetextpost?> getnoteforedit(int id)
        //{
        //    return await _service.getnoteforedit(id); // devuelve los datos de la nota.
        //}

    }
}
