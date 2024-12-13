using NoteApp.Models.Dashboard;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteVoiceDashboard : ContentView
{
	public ObjectDashBoard element { get; set; }
	public NoteVoiceDashboard(ObjectDashBoard e)
	{
		InitializeComponent();
		element = e;
		InitComponent();
	}
	private void InitComponent() { 
		lbTitleVoice.Text = element.titulo;
		txtDescriptionVoice.Text = element.contenido;
		var fecha = DateTime.Parse(element.fechaCreacion).ToString("dd/MM/yy HH:mm");
		lbCreationVoice.Text = element.fechaCreacion;
    }

    public async void OnEditClicked(object sender, EventArgs e)
    {
    }
}