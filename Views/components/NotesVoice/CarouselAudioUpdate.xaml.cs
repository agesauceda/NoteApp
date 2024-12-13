using CommunityToolkit.Maui.Views;
using NoteApp.Models.common;
using NoteApp.Models.VoicePage;
using NoteApp.Services.VoicePage;
using System.Collections.ObjectModel;

namespace NoteApp.Views.components.NotesVoice;

public partial class CarouselAudioUpdate : ContentView
{
	private ObservableCollection<string> list;
	private ObservableCollection<AudioList> audios;
	private AudioPageDelete service;
	public CarouselAudioUpdate()
	{
		InitializeComponent();
		list = new ObservableCollection<string>();
		audios = new ObservableCollection<AudioList>();
		service = new AudioPageDelete();
		audioCarousel.ItemsSource = list;
	}

	public void AddAudio(int id, string path)
	{
		list.Add(path);
		audios.Add(new AudioList() { id = id, path = path });
	}
    public async void OnDeleteButtonClicked(object sender, EventArgs args)
    {
        var tappedElement = ((Button)sender).CommandParameter as string;

        if (tappedElement == null)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo identificar el elemento para eliminar.", "Aceptar");
            return;
        }

        var audioToRemove = audios.FirstOrDefault(x => x.path == tappedElement);
        if (audioToRemove != null)
        {
            if (audioToRemove.id != 0)
            {
                ApiResponse response = await service.DeleteVoice(audioToRemove.id);
                if (response.status.Value)
                {
                    audios.Remove(audioToRemove);
                    list.Remove(tappedElement);
                    await Application.Current.MainPage.DisplayAlert("Eliminación de Audios", response.message, "Aceptar");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el audio de la base de datos.", "Aceptar");
                }
            }
            else
            {
                audios.Remove(audioToRemove);
                list.Remove(tappedElement);
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "El audio no fue encontrado.", "Aceptar");
        }
    }

    public List<AudioList> GetAudios()
	{
		return audios.ToList();
	}

	public void ClearAudios()
	{
		list.Clear();
		audios.Clear();
	}
}