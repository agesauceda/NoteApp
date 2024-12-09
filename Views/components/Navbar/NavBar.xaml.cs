using System.Windows.Input;

namespace NoteApp.Views.components.Navbar;

public partial class NavBar : ContentView
{
    public NavBar()
	{
		InitializeComponent();
        BindingContext = this;
    }

	public ICommand OnNavigateToText => new Command(async () => await Navigation.PushAsync(new TextPage()));
    public ICommand OnNavigateToVoice => new Command(async () => await Navigation.PushAsync(new VoicePage()));
    public ICommand OnNavigateToPhoto => new Command(async () => await Navigation.PushAsync(new PhotoPage()));
    public ICommand OnNavigateToReminder => new Command(async () => await Navigation.PushAsync(new EventPage()));
}