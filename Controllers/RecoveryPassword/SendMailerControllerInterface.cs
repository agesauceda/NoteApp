namespace NoteApp.Controllers.RecoveryPassword
{
    public interface SendMailerControllerInterface
    {
        void SendMail(string cc, string title, string msg);
    }
}
