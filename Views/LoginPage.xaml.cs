namespace NoteApp;
using Views.Interfaces;
using Controllers.Auth;
using System.Windows.Input;
using Utils;

public partial class LoginPage : ContentPage, AuthViewInterface
{
	private AuthController _controller;
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = this;
		_controller = new AuthController((AuthViewInterface)this);
	}

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
		await SendData();
		await Login();
    }
	public async Task Login() {
		string token = Preferences.Get("token", "");
		if (Validator.ValidateString(token)) {
			//await Navigation.PushAsync();
		}
    }
	public async Task SendData() { 
		string user = txtUsername.Text.Trim();
		string pass = txtPassword.Text.Trim();
		if (!String.IsNullOrEmpty(user) && !String.IsNullOrWhiteSpace(user) && !String.IsNullOrEmpty(pass) && !String.IsNullOrWhiteSpace(pass) ) { 
            _controller.Login(user, pass);
		}
		else
		{
			await DisplayAlert("Inicio de Sesion", "Por favor, rellene todos los campos", "OK");
		}
    }
    public async void Login(string response)
    {
		await DisplayAlert("Login", response, "OK");
    }


    public ICommand RegisterView => new Command(async () => ChangeView());

	private async void ChangeView() {
		await Navigation.PushAsync(new RegisterPage());
	}
}