
using NoteApp.Models.TextPage;

namespace NoteApp.Views.Interfaces
{
    public interface TextPageViewInterface
    {
            Task CreateNote(string msg);
            Task UpdateNote(string msg);
            //Task LoadNoteForEdit(NoteTextPOST note);
    }
}