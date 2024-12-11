using NoteApp.Models.Dashboard;

namespace NoteApp.Views.components.Dashboard;

public partial class NoteTextDashboard : ContentView
{

    public ObjectDashBoard element { get; set; }

    public event EventHandler<int> NoteClicked;
    public NoteTextDashboard(ObjectDashBoard e) 
	{
		InitializeComponent();
		element = e;
		InitComponent();
	}
	private void InitComponent() {
		lbTitleText.Text = element.titulo;
		txtDescriptionText.Text = element.contenido;
		lbCreationText.Text = element.fechaCreacion;
	}
    private void OnEditClicked(object sender, EventArgs e)
    {
        if (element?.id != null)
        {
            NoteClicked?.Invoke(this, element.id.Value);
        }
        else
        {
            Console.WriteLine("El ID del elemento no es válido.");
        }
    }
}