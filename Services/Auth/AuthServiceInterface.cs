namespace NoteApp.Services.Auth
{
    public interface AuthServiceInterface
    {
        Task<Models.Auth.Auth> Login(string username, string password);        
    }
}
