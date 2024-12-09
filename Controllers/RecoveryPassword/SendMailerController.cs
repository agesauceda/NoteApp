using NoteApp.Services.RecoveryPassword;
using NoteApp.Views.Interfaces;

namespace NoteApp.Controllers.RecoveryPassword
{
    public class SendMailerController : SendMailerControllerInterface
    {
        private SendMailerService service;
        private RecoveryPasswordPageViewInterface _view;

        public SendMailerController(RecoveryPasswordPageViewInterface view) {
            this._view = view;
            this.service = new SendMailerService();
        }
        public async void SendMail(string cc, string title, string msg)
        {
            var response = await service.SendMailRequest(cc, title, msg);
            _view.ShowSendMailResponse((response.StatusCode == 200) ? "Correo enviado correctamente" : "Error al enviar el correo", response.Data.Content.Status.Value);
        }
    }
}
