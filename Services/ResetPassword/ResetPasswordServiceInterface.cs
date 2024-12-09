using NoteApp.Models.ResetPassword;

namespace NoteApp.Services.ResetPassword
{
    public interface ResetPasswordServiceInterface
    {
        Task<Models.common.ApiResponse> ResetPasswordRequest(ResetPasswordPOST e);
    }
}
