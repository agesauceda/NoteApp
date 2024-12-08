namespace NoteApp.Views;

public partial class DashBoard : ContentPage
{
	public DashBoard()
	{
		InitializeComponent();
	}
    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///Notas");

    }
}