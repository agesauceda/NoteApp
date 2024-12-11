using NoteApp.Models.Dashboard;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteImgDashboard : ContentView
{
	public ObjectDashBoard element;
	public NoteImgDashboard(ObjectDashBoard e)
	{
		InitializeComponent();
        element = e;
        InitComponent();
    }

	private void InitComponent() {
		lbTitleImg.Text = element.titulo;
		txtDescriptionImg.Text = element.contenido;
		var fecha = DateTime.Parse(element.fechaCreacion).ToString("dd/MM/yy HH:mm");
		lbCreationImg.Text = fecha;
    }
}