using NoteApp.Controllers.RecoveryPassword;
using NoteApp.Utils;
using NoteApp.Views.Interfaces;
using Plugin.Firebase.CloudMessaging;

namespace NoteApp.Views;

public partial class RecoveryPasswordPage : ContentPage, RecoveryPasswordPageViewInterface
{
    private SendMailerController _controller;
    private string _code = "";
    private string email;
	public RecoveryPasswordPage()
	{
		InitializeComponent();
        this._controller = new SendMailerController((RecoveryPasswordPageViewInterface)this);
	}

    public async void RecoveryPasswordButton_Clicked(object sender, EventArgs e)
    {
        email = txtEmailRecover.Text;
        if (Validator.ValidateString(email)) {
            this._code = generateCodeRecovery();
            _controller.SendMail(email, "Codigo de Verificacion", $"Código para recuperación de contraseña: {_code}");
        }
    }

    private string generateCodeRecovery() {
        string code = "";
        var n = new Random();
        for (int i = 0; i < 6; i++) {
            code += n.Next(9).ToString();
        }
        return code;
    }
    public async void ShowSendMailResponse(string msg, bool status)
    {
        await DisplayAlert("Recuperar Contraseña", msg, "Aceptar");
        if (status)
        {
            await Navigation.PushAsync(new RecoveryPasswordCode(_code, email));
        }
    }
}