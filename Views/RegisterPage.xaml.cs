using NoteApp.Controllers.RegisterPage;
using NoteApp.Models.RegisterPage;
using NoteApp.Utils;
using NoteApp.Views.Interfaces;

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

    private async Task getData() {
        string user = txtUserReg.Text.Trim();
        string pass = txtPasswdReg.Text.Trim();
        string pass2 = txtPasswdRegChk.Text.Trim();
        string email = txtEmail.Text.Trim();
        string name = txtNombre.Text.Trim();
        string apellido = txtApellido.Text.Trim();
        string telefono = txtPhone.Text.Trim();
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
                FCM = "FCM"
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