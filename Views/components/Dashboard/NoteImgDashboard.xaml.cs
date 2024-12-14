using NoteApp.Models.common;
using NoteApp.Models.Dashboard;
using NoteApp.Models.PhotoPage;
using NoteApp.Services.PhotoPage;
using NoteApp.Services.PhotoPage;
using NoteApp.Utils;
using System.Collections.ObjectModel;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteImgDashboard : ContentView
{
	public ObjectDashBoard element;
	private PhotoPageDelete service;
    private PhotoPageService _service;
	private ObservableCollection<ObjectDashBoard> _list;
    public NoteImgDashboard(ObjectDashBoard e, ObservableCollection<ObjectDashBoard> items)
	{
		InitializeComponent();
        element = e;
        service = new PhotoPageDelete();
        _list = items;
        InitComponent();
    }

	private void InitComponent() {
		lbTitleImg.Text = element.titulo;
		txtDescriptionImg.Text = element.contenido;
		var fecha = DateTime.Parse(element.fechaCreacion).ToString("dd/MM/yy HH:mm");
		lbCreationImg.Text = fecha;
    }

	public async void OnEditRegister(object sender, EventArgs args) {
		await Navigation.PushAsync(new PhotoPageUpdate(element));
	}
    public async void OnDeleteRegister(object sender, EventArgs args)
    {
		ApiResponse response = await service.DeleteNoteImg(element.id.Value);
		if (response.status.Value)
		{
			await Application.Current.MainPage.DisplayAlert("Eliminacíón de Nota", response.message, "Aceptar");
			_list.Remove(element);
		}
		else { 
			await Application.Current.MainPage.DisplayAlert("Eliminacíón de Nota", "Error al eliminar nota", "Aceptar");
        }
    }
    public async void OnShareRegister(object sender, EventArgs args) {
    }
}