using NoteApp.Models.Dashboard;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteTextDashboard : ContentView
{
	public ObjectDashBoard element { get; set; }
	public NoteTextDashboard(ObjectDashBoard e) 
	{
		InitializeComponent();
		element = e;
		InitComponent();
	}
	private void InitComponent() {
		lbTitleText.Text = element.titulo;
		txtDescriptionText.Text = element.contenido;
		lbCreationText.Text = element.fechaCreacion;
	}
}