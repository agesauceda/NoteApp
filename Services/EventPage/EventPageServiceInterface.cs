namespace NoteApp.Services.EventPage
{
    public interface EventPageServiceInterface
    {
        Task<Models.EventPage.ApiResponseReminder> InsertReminder(Models.EventPage.ReminderPOST e);
    }
}
