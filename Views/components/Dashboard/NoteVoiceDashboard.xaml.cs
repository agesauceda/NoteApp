using NoteApp.Models.Dashboard;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteVoiceDashboard : ContentView
{
	public ObjectDashBoard element { get; set; }
	public NoteVoiceDashboard(ObjectDashBoard e)
	{
		InitializeComponent();
		element = e;
	}
	private void InitComponent() { 
		lbTitleVoice.Text = element.titulo;
		txtDescriptionVoice.Text = element.contenido;
		lbCreationVoice.Text = element.fechaCreacion;
    }
}