using System.Windows.Input;

namespace NoteApp.Views.components.Navbar;

public partial class NavBar : ContentView
{
    public NavBar()
	{
		InitializeComponent();
        BindingContext = this;
    }

	public ICommand OnNavigateToText => new Command(async () =>
    {
        await Navigation.PushAsync(new TextPage());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    });
    public ICommand OnNavigateToVoice => new Command(async () => {
        await Navigation.PushAsync(new VoicePage());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    });
    public ICommand OnNavigateToPhoto => new Command(async () => {
        await Navigation.PushAsync(new PhotoPage());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    });
    public ICommand OnNavigateToReminder => new Command(async () => {
        await Navigation.PushAsync(new EventPage());
        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
    });
}