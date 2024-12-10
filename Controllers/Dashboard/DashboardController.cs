using NoteApp.Models.Dashboard;
using NoteApp.Services.Dashboard;
using NoteApp.Views.Interfaces;

namespace NoteApp.Controllers.Dashboard
{
    public class DashboardController : DashboardControllerInterface
    {
        private readonly HttpClient _client = App._client.getClient();
        private readonly DashboardService _dashboardService;
        private readonly DashboardViewInterface _view;
        public DashboardController(DashboardViewInterface view) {
            _view = view;
            _dashboardService = new DashboardService(_client);
        }

        public async Task GetDashboard()
        {
            ApiResponseDashboard response = await _dashboardService.GetDashboard();
            if (response.status.Value) 
            {
                await _view.GetDashboard(response.data.ToList());
            }
            else
            {
                Console.WriteLine(response.message);
            }
        }
    }
}
