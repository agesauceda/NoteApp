using System.Windows.Input;

namespace NoteApp.Views.components.Navbar;

public partial class NavBar : ContentView
{
	public INavigation Navigation { get; set; }
    public NavBar()
	{
		InitializeComponent();
        BindingContext = this;
    }

	private ICommand OnNavigateToText => new Command(async () => await Navigation.PushAsync(new TextPage()));
	private ICommand OnNavigateToVoice => new Command(async () => await Navigation.PushAsync(new VoicePage()));
	private ICommand OnNavigateToPhoto => new Command(async () => await Navigation.PushAsync(new PhotoPage()));
	private ICommand OnNavigateToReminder => new Command(() => Console.WriteLine("Si funciona"));
}