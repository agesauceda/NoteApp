namespace NoteApp.Views;

public partial class DashBoard : ContentPage
{
	public DashBoard()
	{
		InitializeComponent();
	}
    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TextPage());
    }
}