namespace NoteApp.Controllers.NotePage
{
    public interface TextPageControllerInterface
    {
        Task CreateNote(Models.TextPage.NoteTextPOST e);
        Task UpdateNote(int id, Models.TextPage.NoteTextPOST e);
        Task<Models.TextPage.NoteTextPOST?> GetNoteForEdit(int id);
    }
}