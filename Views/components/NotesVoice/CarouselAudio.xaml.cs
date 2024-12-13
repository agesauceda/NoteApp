using CommunityToolkit.Maui.Views;
using NoteApp.Models.common;
using NoteApp.Models.VoicePage;
using NoteApp.Services.VoicePage;
using Plugin.Maui.Audio;
using System.Collections.ObjectModel;

namespace NoteApp.Views.components.NotesVoice;

public partial class CarouselAudio : ContentView
{
    private ObservableCollection<string> list;

    private AudioPageDelete service;

    public CarouselAudio()
    {
        InitializeComponent();
        list = new ObservableCollection<string>();
        audioCarousel.ItemsSource = list;
    }

    public async void AddAudio(string filePath)
    {
        list.Add(filePath);
    }
    public async void OnDeleteButtonClicked(object sender, EventArgs args)
    {
        if (sender is Button deleteButton)
        {
            string filePath = deleteButton.CommandParameter as string;

            if (!string.IsNullOrEmpty(filePath))
            {
                bool confirmDelete = await Application.Current.MainPage.DisplayAlert(
                    "Confirmación",
                    $"¿Deseas eliminar este audio?",
                    "Sí",
                    "No");

                if (confirmDelete)
                {
                    RemoveAudio(filePath);
                }
            }
        }
    }

    private void RemoveAudio(string filePath)
    {
        list.Remove(filePath);
    }

    public List<string> GetAudios()
    {
        return list.ToList();
    }

    public void ClearAudios()
    {
        list.Clear();
    }



}