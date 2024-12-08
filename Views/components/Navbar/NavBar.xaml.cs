namespace NoteApp.Views.components.Navbar;

public partial class NavBar : ContentView
{
	public NavBar()
	{
		InitializeComponent();
	}

	private async void OnNavigateToText(object sender, EventArgs args) {
		await Shell.Current.GoToAsync("///Text");
	}

	private async void OnNavigateToVoice(object sender, EventArgs args) {
		await Shell.Current.GoToAsync("///Voice");
	}
	private async void OnNavigateToPhoto(object sender, EventArgs args) {
		await Shell.Current.GoToAsync("///Photo");
	}
	private async void OnNavigateToReminder(object sender, EventArgs args) {
		await Shell.Current.GoToAsync("///Reminder");
	}
}