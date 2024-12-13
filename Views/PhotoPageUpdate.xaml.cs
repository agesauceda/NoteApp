using NoteApp.Controllers.PhotoPage;
using NoteApp.Models.Dashboard;
using NoteApp.Models.PhotoPage;
using NoteApp.Utils;
using NoteApp.Views.Interfaces;

namespace NoteApp.Views;

public partial class PhotoPageUpdate : ContentPage, PhotoPageViewInterface
{
	private ImgNotePUT post;
    private List<ImgPOST> images;
    private readonly PhotoPageController _controller;
    private ObjectDashBoard obj;
    public PhotoPageUpdate(ObjectDashBoard e)
	{
		InitializeComponent();
        images = new List<ImgPOST>();
        obj = e;
        _controller = new PhotoPageController((PhotoPageViewInterface) this);
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        txtTitulo.Text = obj.titulo;
        txtDescription.Text = obj.contenido;
        await _controller.getImages(obj.id.Value);
    }
    private async void OnUpdateButtonClicked(object sender, EventArgs e)
    {
        if (Validator.ValidateString(txtTitulo.Text) && Validator.ValidateString(txtDescription.Text)) {
            var list = CarrouselObject.GetImages();
            for (int i = 0; i < list.Count; i++) {
                images.Add(new ImgPOST() { id = list[i].id,  img = Converters.ConvertToBase64(list[i].path).Result});
            }
            if (images.Count > 0) { 
                post = new ImgNotePUT() { 
                    titulo = txtTitulo.Text,
                    descripcion = txtDescription.Text,
                    fotos = images
                };
                await _controller.UpdateNotePhoto(post, obj.id.Value);
                flushData();
            }
        }
    }
    private void flushData() {
        post = new ImgNotePUT();
        txtDescription.Text = "";
        txtTitulo.Text = "";
        images.Clear();
        CarrouselObject.ClearImages();
    }
    private async void OnPickPhotoButtonClicked(object sender, EventArgs e)
	{
		var res = await RequestPermissions.CheckPermissionCameraAsync();
		if (res != null) {
            CarrouselObject.AddImage(0,res);
        }
    }

    public async Task insertNotePhoto(string msg)
    {
    }

    public async Task getImages(List<ImgNoteGET> imgs)
    {
        if(imgs != null)
        {
            for (int i = 0; i < imgs.Count; i++)
            {
                var path = await Converters.ConvertBase64ToFile(imgs[i].base64, imgs[i].id.Value);
                CarrouselObject.AddImage(imgs[i].id.Value, path);
            }
        }
    }

    public async Task updateNotePhoto(string msg)
    {
        await DisplayAlert("Actualización de Nota", msg, "Aceptar");
    }
}