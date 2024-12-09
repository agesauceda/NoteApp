using Microsoft.Maui.ApplicationModel.Communication;
using NoteApp.Controllers.RecoveryPassword;
using NoteApp.Utils;
using NoteApp.Views.Interfaces;
using Plugin.Firebase.CloudMessaging;
using System.Windows.Input;

namespace NoteApp.Views;

public partial class RecoveryPasswordCode : ContentPage, RecoveryPasswordPageViewInterface
{
    private SendMailerController _controller;
    private string _code = "";
    private string _email;
	public RecoveryPasswordCode(string code, string email)
	{
		InitializeComponent();
        BindingContext = this;
        this._code = code;
        this._email = email;
        this._controller = new SendMailerController((RecoveryPasswordPageViewInterface)this);
	}
    public async void VerifyCode_Clicked(object sender, EventArgs e)
    {
        string codeEntry = txtCodeVerify.Text;
        if (Validator.ValidateString(codeEntry) && _code == codeEntry)
        {
            await Navigation.PushAsync(new ResetPassword(_email));
        }
        else {
            await DisplayAlert("C�digo de Verificaci�n", "C�digo incorrecto", "Aceptar");
        }
    }
    public ICommand ResendEmail => new Command(() => _controller.SendMail(_email, "Codigo de Verificacion", $"C�digo para recuperaci�n de contrase�a: {_code}"));
    public async void ShowSendMailResponse(string msg, bool status)
    {
        await DisplayAlert("Recuperar Contrase�a", msg, "Aceptar");
    }
}