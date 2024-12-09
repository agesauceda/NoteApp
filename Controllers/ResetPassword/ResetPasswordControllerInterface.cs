using NoteApp.Models.ResetPassword;

namespace NoteApp.Controllers.ResetPassword
{
    public interface ResetPasswordControllerInterface
    {
        void ChangePassword(ResetPasswordPOST e);
    }
}
