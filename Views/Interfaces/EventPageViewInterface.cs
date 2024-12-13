using NoteApp.Models.EventPage;

namespace NoteApp.Views.Interfaces
{

    public interface EventPageViewInterface
    {
        Task InsertReminder(string msg);
        Task GetReminder(ReminderGET? reminder);
        Task UpdateReminder(ReminderPUT reminder);
    }

}