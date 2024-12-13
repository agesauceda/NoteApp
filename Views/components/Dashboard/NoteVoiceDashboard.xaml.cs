using NoteApp.Models.common;
using NoteApp.Models.Dashboard;
using NoteApp.Services.VoicePage;
using System.Collections.ObjectModel;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteVoiceDashboard : ContentView
{
	public ObjectDashBoard element;
	private AudioPageDelete service;
	private ObservableCollection<ObjectDashBoard> _list;
	public NoteVoiceDashboard(ObjectDashBoard e, ObservableCollection<ObjectDashBoard> items)
	{
		InitializeComponent();
		element = e;
		service = new AudioPageDelete();
		_list = items;
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
        _list.Remove(element);

    }

}