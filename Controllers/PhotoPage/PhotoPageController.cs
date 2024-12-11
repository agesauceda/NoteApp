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
        public Task insertNotePhoto(NotesImgPOST e)
        {
            return Task.Run(async () =>
            {
                ApiResponseNoteImgPOST response = await service.InsertNoteImg(e);
                if (response != null)
                {
                    await _view.insertNotePhoto(response.message);
                }
            });
        }
    }
}
