using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Services.NotePage
{
    public interface TextPageServiceInterface
    {
        Task<Models.common.ApiResponse> CreateNote(Models.TextPage.NoteTextPOST e);
        Task<Models.common.ApiResponse> UpdateNote(int id, Models.TextPage.NoteTextPOST e);
        Task<Models.TextPage.NoteTextPOST?> GetNoteForEdit(int id);
    }
}