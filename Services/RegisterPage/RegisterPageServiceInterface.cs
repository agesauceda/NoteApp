namespace NoteApp.Services.RegisterPage
{
    public interface RegisterPageServiceInterface
    {
        Task<Models.common.ApiResponse> RegisterUser(Models.RegisterPage.UserPOST e);
    }
}
