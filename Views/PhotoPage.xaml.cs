using NoteApp.Controllers.PhotoPage;
using NoteApp.Models.PhotoPage;
using NoteApp.Utils;
using NoteApp.Views.Interfaces;

namespace NoteApp.Views;

public partial class PhotoPage : ContentPage, PhotoPageViewInterface
{
	private NotesImgPOST post;
    private List<string> images;
    private readonly PhotoPageController _controller;
    public PhotoPage()
	{
		InitializeComponent();
        images = new List<string>();
        _controller = new PhotoPageController((PhotoPageViewInterface) this);
    }
	private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (Validator.ValidateString(txtTitulo.Text) && Validator.ValidateString(txtDescription.Text)) {
            var list = CarrouselObject.GetImages();
            for (int i = 0; i < list.Count; i++) {
                images.Add(Converters.ConvertToBase64(list[i]).Result);
            }
            post = new NotesImgPOST() { 
                titulo = txtTitulo.Text,
                descripcion = txtDescription.Text,
                fotos = images
            };
            await _controller.insertNotePhoto(post);
            flushData();
        }
    }
    private void flushData() {
        post = new NotesImgPOST();
        txtDescription.Text = "";
        txtTitulo.Text = "";
        images.Clear();
        CarrouselObject.ClearImages();
    }
    private async void OnPickPhotoButtonClicked(object sender, EventArgs e)
	{
		var res = await RequestPermissions.CheckPermissionCameraAsync();
		if (res != null) {
            CarrouselObject.AddImage(res);
        }
    }

    public async Task insertNotePhoto(string msg)
    {
        await DisplayAlert("Registro de Nota", msg, "Aceptar");
    }

    public Task getImages(List<ImgNoteGET> imgs)
    {
        return Task.CompletedTask;
    }

    public Task updateNotePhoto(string msg)
    {
        return Task.CompletedTask;
    }
}