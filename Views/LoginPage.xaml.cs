namespace NoteApp;
using Views.Interfaces;
using Controllers.Auth;
using System.Windows.Input;
using Utils;
using Plugin.Firebase.CloudMessaging;
using NoteApp.Views;

public partial class LoginPage : ContentPage, AuthViewInterface
{
	private AuthController _controller;
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = this;
		_controller = new AuthController((AuthViewInterface)this);
	}

    protected override async void OnAppearing()
    {
		base.OnAppearing();
        await RequestPermissions.CheckPermissionNotification();
        await isLogin();
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
		await SendData();
		await Login();
		await isLogin();
    }
	private async Task isLogin() {
		if (await App._queueLogin.ValidateInitialSession()) {
            await Navigation.PushAsync(new DashBoard());
        }
	}
	public async Task Login() {
		string token = Preferences.Get("token", "");
		if (Validator.ValidateString(token)) {
			await Navigation.PushAsync(new DashBoard());
		}
    }
	public async Task SendData() { 
		string user = txtUsername.Text;
		string pass = txtPassword.Text;
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

	public ICommand RecoveryPassword => new Command(async () => RecoveryPasswordView());

	private async void RecoveryPasswordView()
    {
        await Navigation.PushAsync(new RecoveryPasswordPage());
    }
}