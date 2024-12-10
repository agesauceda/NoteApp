using NoteApp.Controllers.Dashboard;
using NoteApp.Models.Dashboard;
using NoteApp.Views.components.Dashboard;
using NoteApp.Views.Interfaces;

namespace NoteApp.Views;

public partial class DashBoard : ContentPage, DashboardViewInterface
{
    private readonly DashboardControllerInterface _controller;
    private List<ObjectDashBoard> _list = new List<ObjectDashBoard>();
	public DashBoard()
	{
		InitializeComponent();
        _controller = new DashboardController((DashboardViewInterface)this);
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (_list.Count <= 0) { 
            await _controller.GetDashboard();
        }
    }
    public Task GetDashboard(List<ObjectDashBoard> list)
    {
        _list = list;
        if(list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++) {
                switch (list[i].tipoNota) {
                    case "TEXTO":
                        DashboardContent.Add(new NoteTextDashboard(list[i]));
                        break;
                    case "IMAGEN":
                        DashboardContent.Add(new NoteImgDashboard(list[i]));
                        break;
                    case "AUDIO":
                        DashboardContent.Add(new NoteVoiceDashboard(list[i]));
                        break;
                    case "RECORDATORIOS":
                        DashboardContent.Add(new ReminderDashboard(list[i]));
                        break;
                    default:
                        break;
                }
            }
        }
        return Task.CompletedTask;
    }

    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TextPage());
    }
}