using NoteApp.Models.common;
using NoteApp.Models.PhotoPage;
using NoteApp.Services.PhotoPage;
using NoteApp.Views.Interfaces;

namespace NoteApp.Controllers.PhotoPage
{
    public class PhotoPageController : PhotoPageControllerInterface
    {
        private readonly HttpClient client = App._client.getClient();
        private readonly PhotoPageService service;
        private readonly PhotoPageViewInterface _view;
        public PhotoPageController(PhotoPageViewInterface view) {
            service = new PhotoPageService(client);
            _view = view;
        }

        public async Task getImages(int id)
        {
            ApiResponseNoteImg response = await service.GetImages(id);
            if (response.status.Value)
            {
                var imgs = response.data.ToList();
                await _view.getImages(imgs);
            }
            else {
                await _view.getImages(null);
            }
        }

        public async Task insertNotePhoto(NotesImgPOST e)
        {
            ApiResponseNoteImgPOST response = await service.InsertNoteImg(e);
            if (response.status.Value)
            {
                await _view.insertNotePhoto(response.message);
            }
            else { 
                await _view.insertNotePhoto("Error al crear la nota");
            }
        }

        public async Task UpdateNotePhoto(ImgNotePUT e, int id)
        {
            ApiResponse response = await service.UpdateNoteImg(e, id);
            if (response.status.Value)
            {
                await _view.updateNotePhoto(response.message);
            }
            else
            {
                await _view.updateNotePhoto("Error al actualizar la nota");
            }
        }
    }
}
