using System.Collections.ObjectModel;

namespace NoteApp.Views.components.NotesImg;

public partial class CarouselView : ContentView
{
	private ObservableCollection<string> list;
	public CarouselView()
	{
		InitializeComponent();
        list = new ObservableCollection<string>();
        Carrousel.ItemsSource = list;
    }

	public void AddImage(string path)
    {
        list.Add(path);
    }
    public void OnDeleteTapped(object sender, TappedEventArgs args) {
        var obj = (Image)sender;
        RemoveImage(obj.Source.ToString().Split(" ")[1]);
    }

    private void RemoveImage(string path) { 
        list.Remove(path);
    }
    public List<string> GetImages()
    {
        return list.ToList();
    }
    public void ClearImages()
    {
        list.Clear();
    }
}