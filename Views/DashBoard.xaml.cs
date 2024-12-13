using NoteApp.Controllers.Dashboard;
using NoteApp.Models.Dashboard;
using NoteApp.Models.TextPage;
using NoteApp.Views.components.Dashboard;
using NoteApp.Views.Interfaces;
using System.Collections.ObjectModel;

namespace NoteApp.Views;

public partial class DashBoard : ContentPage, DashboardViewInterface
{
    private readonly DashboardControllerInterface _controller;
    //private List<ObjectDashBoard> _list = new List<ObjectDashBoard>();
    private NoteTextPOST notasText;
    private ObservableCollection<ObjectDashBoard> _list = new ObservableCollection<ObjectDashBoard>();
	public DashBoard()
	{
		InitializeComponent();
        _controller = new DashboardController((DashboardViewInterface)this);
        _list.CollectionChanged += OnCollectionChanged;
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _controller.GetDashboard();
    }

    public Task GetDashboard(List<ObjectDashBoard> list)
    {
        _list = new ObservableCollection<ObjectDashBoard>(list);
        _list.CollectionChanged += OnCollectionChanged;
        DashboardContent.Clear();
        if(list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++) {
                AddDashboardItem(_list[i]);
            }
        }
        return Task.CompletedTask;
    }

    private void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
        {
            foreach (ObjectDashBoard newItem in e.NewItems)
            {
                AddDashboardItem(newItem);
            }
        }
        else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
        {
            foreach (ObjectDashBoard oldItem in e.OldItems)
            {
                RemoveDashboardItem(oldItem);
            }
        }
    }
    private void AddDashboardItem(ObjectDashBoard item)
    {
        switch (item.tipoNota)
        {
            case "TEXTO":
                DashboardContent.Add(new NoteTextDashboard(item, _list));
                break;
            case "IMAGEN":
                DashboardContent.Add(new NoteImgDashboard(item, _list));
                break;
            case "AUDIO":
                DashboardContent.Add(new NoteVoiceDashboard(item, _list));
                break;
            case "RECORDATORIOS":
                DashboardContent.Add(new ReminderDashboard(item, _list));
                break;
            default:
                break;
        }
    }
    private void RemoveDashboardItem(ObjectDashBoard item)
    {
        try
        {
            var dashboardItem = DashboardContent.FirstOrDefault(x =>
            { 
                if(x is NoteTextDashboard textDashboard)
                    return textDashboard.element.id == item.id;
                if (x is NoteImgDashboard imgDashboard)
                    return imgDashboard.element.id == item.id;
                if (x is NoteVoiceDashboard voiceDashboard)
                    return voiceDashboard.element.id == item.id;
                if (x is ReminderDashboard reminderDashboard)
                    return reminderDashboard.element.id == item.id;
                return false;
            });
            if (dashboardItem != null)
            {
                DashboardContent.Remove(dashboardItem);
            }
        }
        catch (InvalidCastException ex) {
            Console.WriteLine(ex.Message);
        }
    }
    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TextPage());
    }

    private async void LogoutClicked(object sender, EventArgs e)
    {
        Preferences.Clear();
        await Navigation.PushAsync(new LoginPage());
    }
}