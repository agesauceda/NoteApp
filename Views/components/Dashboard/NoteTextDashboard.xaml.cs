using Microsoft.Maui.Controls;
using NoteApp.Models.common;
using NoteApp.Models.Dashboard;
using NoteApp.Models.TextPage;
using NoteApp.Services.TextPage;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteTextDashboard : ContentView
{

    public ObjectDashBoard element { get; set; }
    private TextPageDelete service;

    public event EventHandler<int> NoteClicked;
    public NoteTextDashboard(ObjectDashBoard e) 
	{
		InitializeComponent();
		element = e;
        service = new TextPageDelete();
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

    public async void OnDeleteClicked(object sender, EventArgs e)
    {
        ApiResponse response = await service.DeleteNoteText(element.id.Value);
        await Application.Current.MainPage.DisplayAlert("Eliminacíón de Nota", (response.status.Value) ? response.message : "No se pudo eliminar la nota", "Aceptar");
    }
}
