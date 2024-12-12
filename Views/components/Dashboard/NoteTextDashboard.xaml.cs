using Microsoft.Maui.Controls;
using NoteApp.Models.Dashboard;
using NoteApp.Models.TextPage;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteTextDashboard : ContentView
{

    public ObjectDashBoard element { get; set; }

    public event EventHandler<int> NoteClicked;
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
    public async void OnEditClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TextPageUpdate(element));
    }
}
