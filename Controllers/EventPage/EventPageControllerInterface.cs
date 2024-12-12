using NoteApp.Models.EventPage;

namespace NoteApp.Controllers.EventPage
{
    public interface EventPageControllerInterface
    {
        Task InsertReminder(ReminderPOST e);
    }
}
