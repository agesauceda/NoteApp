using NoteApp.Models.EventPage;

namespace NoteApp.Services.EventPage
{
    public interface EventPageServiceInterface
    {
        Task<Models.EventPage.ApiResponseReminder> InsertReminder(Models.EventPage.ReminderPOST e);
        Task<ApiResponseReminder> GetReminder(int id);
        Task<ApiResponseReminder> UpdateReminder(ReminderPUT e, int id);
    }
}
