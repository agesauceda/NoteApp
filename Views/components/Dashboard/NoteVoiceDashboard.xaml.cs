using NoteApp.Models.common;
using NoteApp.Models.Dashboard;
using NoteApp.Services.VoicePage;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteVoiceDashboard : ContentView
{
	public ObjectDashBoard element;
	private AudioPageDelete service;
	public NoteVoiceDashboard(ObjectDashBoard e)
	{
		InitializeComponent();
		element = e;
		service = new AudioPageDelete();
		InitComponent();
	}
	private void InitComponent() { 
		lbTitleVoice.Text = element.titulo;
		txtDescriptionVoice.Text = element.contenido;
		var fecha = DateTime.Parse(element.fechaCreacion).ToString("dd/MM/yy HH:mm");
		lbCreationVoice.Text = fecha;
    }

    public async void OnEditClicked(object sender, EventArgs args)
	{
		await Navigation.PushAsync(new VoicePageUpdate(element));	
	}

	public async void OnDeleteClicked(object sender, EventArgs args)
	{
		ApiResponse response = await service.DeleteNoteAudio(element.id.Value);
        await Application.Current.MainPage.DisplayAlert("Eliminacíón de Nota", (response.status.Value) ? response.message : "No se pudo eliminar la nota", "Aceptar");
    }

}