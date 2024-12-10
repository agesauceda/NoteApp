using NoteApp.Models.Dashboard;

namespace NoteApp.Views.Interfaces
{
    public interface DashboardViewInterface
    {
        Task GetDashboard(List<ObjectDashBoard> list);
    }
}
