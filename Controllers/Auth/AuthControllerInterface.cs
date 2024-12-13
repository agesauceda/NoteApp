namespace NoteApp.Controllers.Auth
{
    public interface AuthControllerInterface
    {
        Task Login(string username, string password);
    }
}
