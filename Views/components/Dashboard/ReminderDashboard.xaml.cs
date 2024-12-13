using NoteApp.Models.Dashboard;
using NoteApp.Models.EventPage;

namespace NoteApp.Views.components.Dashboard;

public partial class ReminderDashboard : ContentView
{
	public ObjectDashBoard element { get; set; }
	public ReminderDashboard(ObjectDashBoard e)
	{
		InitializeComponent();
		element = e;
		InitComponent();
	}
	private void InitComponent() {
		lbTitleReminder.Text = element.titulo;
		dateIniReminder.Text = DateTime.Parse(element.fechaInicio).ToString("dd/MM/yyyy");
		timeIniReminder.Text = DateTime.Parse(element.fechaInicio).ToString("HH:mm");
		dateFinReminder.Text = DateTime.Parse(element.fechaFinal).ToString("dd/MM/yyyy");
		timeFinReminder.Text = DateTime.Parse(element.fechaFinal).ToString("HH:mm");
		txtDescriptionReminder.Text = element.contenido;
		lbCreationReminder.Text = DateTime.Parse(element.fechaCreacion).ToString("dd/MM/yy HH:mm");
    }

	public async void EditReminder(object sender, EventArgs args) {
		await Navigation.PushAsync(new EventPageUpdate(element));
		
	}
    public async void DeleteReminder(object sender, EventArgs args)
    {
	
    }

}