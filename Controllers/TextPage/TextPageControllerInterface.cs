namespace NoteApp.Controllers.NotePage
{
    public interface TextPageControllerInterface
    {
        Task CreateNote(Models.TextPage.NoteTextPOST e);
    }
}