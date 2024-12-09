using NoteApp.Models.ResetPassword;
using NoteApp.Services.ResetPassword;
using NoteApp.Views.Interfaces;

namespace NoteApp.Controllers.ResetPassword
{
    public class ResetPasswordController : ResetPasswordControllerInterface
    {
        private readonly HttpClient _client;
        private ResetPasswordService _service;
        private ResetPasswordViewInterface _view;
        public ResetPasswordController(ResetPasswordViewInterface view) {
            _client = App._client.getClient();
            _service = new ResetPasswordService(_client);
            _view = view;
        }
        public async void ChangePassword(ResetPasswordPOST e)
        {
            var response = await _service.ResetPasswordRequest(e);
            _view.ShowResponseResetPassword(response.message, response.status.Value);
        }
    }
}
