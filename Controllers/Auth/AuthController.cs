namespace NoteApp.Controllers.Auth
{
    public class AuthController : AuthControllerInterface
    {
        private readonly HttpClient _client = App._client.getClient();
        private Services.Auth.AuthService _service;
        private Views.Interfaces.AuthViewInterface _view;
        public AuthController(Views.Interfaces.AuthViewInterface view) {
            this._view = view;
            this._service = new Services.Auth.AuthService(_client);
        }

        public async Task Login(string username, string password) {
            Models.Auth.Auth e = await _service.Login(username, password);
            if (e != null)
            {
                Preferences.Set("token", e.token);
                _view.Login("Sesión Iniciada Correctamente");
            }
            else
            {
                _view.Login("Login failed");
            }
        }
    }
}
