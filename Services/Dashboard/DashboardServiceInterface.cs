using NoteApp.Models.Dashboard;

namespace NoteApp.Services.Dashboard
{
    public interface DashboardServiceInterface
    {
        Task<ApiResponseDashboard> GetDashboard();
    }
}
