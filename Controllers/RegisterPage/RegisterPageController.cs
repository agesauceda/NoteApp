using NoteApp.Models.common;
using NoteApp.Models.RegisterPage;
using NoteApp.Services.MailSender;
using NoteApp.Services.RegisterPage;
using NoteApp.Views.Interfaces;

namespace NoteApp.Controllers.RegisterPage
{
    public class RegisterPageController : RegisterPageControllerInterface
    {
        private readonly HttpClient _client = App._client.getClient();
        private RegisterPageService _service;
        private RegisterPageViewInterface _view;
        private Mailer mailer = new Mailer();
        public RegisterPageController(RegisterPageViewInterface view) { 
            this._service = new RegisterPageService(_client);
            this._view = view;
        }
        public async Task RegisterUser(UserPOST e)
        {
            var code = generateCodeRecovery();
            await mailer.MailRequest(e.email, "Registro de usuario", "Código de Verificación de Registro: "+code);
            string result = await App.Current.MainPage.DisplayPromptAsync("Codigó de Verificación", "Ingrese el código de Verificación:", accept: "Verificar", cancel: "Cancelar", placeholder: "xxxxxxx", maxLength: 6, keyboard: Keyboard.Numeric);
            if (result == code) {
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

        private string generateCodeRecovery()
        {
            string code = "";
            var n = new Random();
            for (int i = 0; i < 6; i++)
            {
                code += n.Next(9).ToString();
            }
            return code;
        }

    }
}
