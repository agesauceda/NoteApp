using NoteApp.Models.common;
using NoteApp.Models.Dashboard;
using NoteApp.Services.PhotoPage;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteImgDashboard : ContentView
{
	public ObjectDashBoard element;
	private PhotoPageDelete service;
	public NoteImgDashboard(ObjectDashBoard e)
	{
		InitializeComponent();
        element = e;
        service = new PhotoPageDelete();
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
		await Application.Current.MainPage.DisplayAlert("Eliminacíón de Nota", (response.status.Value) ? response.message : "No se pudo eliminar la nota", "Aceptar");
    }
}