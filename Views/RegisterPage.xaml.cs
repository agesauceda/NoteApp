using NoteApp.Controllers.RegisterPage;
using NoteApp.Models.RegisterPage;
using NoteApp.Utils;
using NoteApp.Views.Interfaces;
using Plugin.Firebase.CloudMessaging;

namespace NoteApp;

public partial class RegisterPage : ContentPage, RegisterPageViewInterface
{
    private RegisterPageController _controller;
    private UserPOST e;
    private string Gender = "";
    public RegisterPage()
	{
		InitializeComponent();
        this._controller = new RegisterPageController((RegisterPageViewInterface)this);
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await getTokenFCM();
    }

    private async Task getTokenFCM()
    {
        try
        {
            await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
            var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
            Preferences.Set("tokenFCM", token);
            Console.WriteLine(Preferences.Get("tokenFCM", ""));
        }
        catch (Exception e)
        {
            await DisplayAlert("", e.Message, "Ok");
        }
    }
    private async Task getData() {
        string user = txtUserReg.Text;
        string pass = txtPasswdReg.Text;
        string pass2 = txtPasswdRegChk.Text;
        string email = txtEmail.Text;
        string name = txtNombre.Text;
        string apellido = txtApellido.Text;
        string telefono = txtPhone.Text;
        string fecha = txtFechaNacimiento.Date.ToString("yyyy-MM-dd");
        if(Validator.ValidateString(user) && Validator.ValidateString(pass) && Validator.ValidateString(pass2) && Validator.ValidateString(email)
            && Validator.ValidateString(name) && Validator.ValidateString(apellido) && Validator.ValidateString(telefono) && Validator.ValidateString(fecha)
            && Validator.ValidateString(email) && Validator.PasswordMatch(pass, pass2) && Validator.ValidateString(telefono))
        {
            e = new UserPOST
            {
                user = user,
                passwd = pass,
                email = email,
                nombre = name,
                fecha = fecha,
                apellido = apellido,
                telefono = telefono,
                genero = Gender,
                fcm = Preferences.Get("tokenFCM", "")
            };
            await _controller.RegisterUser(e);
            flushData();
        }
        else
        {
            await DisplayAlert("Error", "Datos incorrectos", "OK");
        }
    }
    private void flushData() {
        e = null;
        Gender = "";
        txtUserReg.Text = "";
        txtPasswdReg.Text = "";
        txtPasswdRegChk.Text = "";
        txtEmail.Text = "";
        txtNombre.Text = "";
        txtApellido.Text = "";
        txtPhone.Text = "";
        cboGen.SelectedIndex = 0;
    }
    public async Task RegisterUser(string msg)
    {
        await DisplayAlert("Registro", msg, "OK");
    }

    public async void SelectGen_SelectedIndexChanged(object sender, EventArgs e) {
        Gender = cboGen.SelectedItem.ToString().Substring(0,1).ToUpper();
        Console.WriteLine(Gender);

    }
    public async void RegisterUser_Clicked(object sender, EventArgs e) {
        await getData();
    }
}