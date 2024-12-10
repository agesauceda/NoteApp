using NoteApp.Models.Dashboard;

namespace NoteApp.Views.components.Dashboard;

public partial class ReminderDashboard : ContentView
{
	public ObjectDashBoard element { get; set; }
	public ReminderDashboard(ObjectDashBoard e)
	{
		InitializeComponent();
		element = e;
	}
	private void InitComponent() {
		lbTitleReminder.Text = element.titulo;
		dateIniReminder.Text = element.fechaInicio;
		timeIniReminder.Text = element.fechaInicio;
		dateFinReminder.Text = element.fechaFinal;
		timeFinReminder.Text = element.fechaFinal;
		txtDescriptionReminder.Text = element.contenido;
		lbCreationReminder.Text = element.fechaCreacion;

    }
}