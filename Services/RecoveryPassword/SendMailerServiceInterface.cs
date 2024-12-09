using NoteApp.Models.common.ResponseMailer;

namespace NoteApp.Services.RecoveryPassword
{
    public interface SendMailerServiceInterface
    {
        Task<ApiResponseMail> SendMailRequest(string cc, string title, string msg);
    }
}
