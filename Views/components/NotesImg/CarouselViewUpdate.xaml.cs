using NoteApp.Models.common;
using NoteApp.Models.PhotoPage;
using NoteApp.Services.PhotoPage;
using System.Collections.ObjectModel;

namespace NoteApp.Views.components.NotesImg;

public partial class CarouselViewUpdate : ContentView
{
    private ObservableCollection<string> list;
    private ObservableCollection<ImgList> img;
    private PhotoPageDelete service;
    public CarouselViewUpdate()
    {
        InitializeComponent();
        list = new ObservableCollection<string>();
        img = new ObservableCollection<ImgList>();
        service = new PhotoPageDelete();
        Carrousel.ItemsSource = list;
    }

	public void AddImage(int id, string path)
    {
        list.Add(path);
        img.Add(new ImgList() { id = id, path = path});
    }
    public async void OnDeleteTapped(object sender, TappedEventArgs args)
    {
        try
        {
            var obj = (Image)sender;
            var source = obj.Source.ToString();
            Console.WriteLine($"Source completo: {source}");
            string path = source.Split(" ")[1];
            var element = img.Where(x => x.path == path).Single();
            if (element.id != 0)
            {
                ApiResponse response = await service.DeleteImg(element.id);
                if (response.status.Value)
                {
                    img.Remove(element);
                    list.Remove(path);
                    await Application.Current.MainPage.DisplayAlert("Eliminación de Imagen", response.message, "Aceptar");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Eliminación de Imagen", "Error al eliminar imagen", "Aceptar");
                }
            }
            else {
                await Application.Current.MainPage.DisplayAlert("Eliminación de Imagen", "Eliminar ", "Aceptar");
                img.Remove(element);
                list.Remove(path);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en OnDeleteTapped: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error", "Ocurrió un error inesperado", "Aceptar");
        }
    }

    public List<ImgList> GetImages()
    {
        return img.ToList(); ;
    }
    public void ClearImages()
    {
        list.Clear();
        img.Clear();
    }
}