using NoteApp.Models.common;
using NoteApp.Models.Dashboard;
using NoteApp.Models.EventPage;
using NoteApp.Services.EventPage;
using System.Collections.ObjectModel;

namespace NoteApp.Views.components.Dashboard;

public partial class ReminderDashboard : ContentView
{
	public ObjectDashBoard element { get; set; }
	private EventPageDelete service;
    private ObservableCollection<ObjectDashBoard> _list;

    public ReminderDashboard(ObjectDashBoard e, ObservableCollection<ObjectDashBoard> items)
	{
		InitializeComponent();
		element = e;
		service = new EventPageDelete();
        _list = items;
        InitComponent();
	}
	private void InitComponent() {
		lbTitleReminder.Text = element.titulo;
		dateIniReminder.Text = DateTimeOffset.Parse(element.fechaInicio).ToString("dd/MM/yyyy");
		timeIniReminder.Text = DateTimeOffset.Parse(element.fechaInicio).ToString("HH:mm");
        dateFinReminder.Text = DateTimeOffset.Parse(element.fechaFinal).ToString("dd/MM/yyyy");
		timeFinReminder.Text = DateTimeOffset.Parse(element.fechaFinal).ToString("HH:mm");
        txtDescriptionReminder.Text = element.contenido;
		lbCreationReminder.Text = DateTime.Parse(element.fechaCreacion).ToString("dd/MM/yy HH:mm");
		
    }

    public async void EditReminder(object sender, EventArgs args) {
		await Navigation.PushAsync(new EventPageUpdate(element));
		
	}
    public async void DeleteReminder(object sender, EventArgs args)
    {
		ApiResponse response = await service.DeleteNoteEvent(element.id.Value);
        await Application.Current.MainPage.DisplayAlert("Eliminacíón de Nota", (response.status.Value) ? response.message : "No se pudo eliminar la nota", "Aceptar");
        _list.Remove(element);
    }

}