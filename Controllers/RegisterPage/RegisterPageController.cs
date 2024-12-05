using NoteApp.Models.common;
using NoteApp.Models.RegisterPage;
using NoteApp.Services.RegisterPage;
using NoteApp.Views.Interfaces;

namespace NoteApp.Controllers.RegisterPage
{
    public class RegisterPageController : RegisterPageControllerInterface
    {
        private readonly HttpClient _client = App._client.getClient();
        private RegisterPageService _service;
        private RegisterPageViewInterface _view;
        public RegisterPageController(RegisterPageViewInterface view) { 
            this._service = new RegisterPageService(_client);
            this._view = view;
        }
        public async Task RegisterUser(UserPOST e)
        {
            ApiResponse response = await _service.RegisterUser(e);
            if (response.status.Value)
            {
                await _view.RegisterUser(response.message);
            }
            else
            {
                await _view.RegisterUser("Error al registrar usuario");
            }
        }
    }
}
