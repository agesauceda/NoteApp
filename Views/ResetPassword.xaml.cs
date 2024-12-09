using Microsoft.Maui.ApplicationModel.Communication;
using NoteApp.Controllers.RecoveryPassword;
using NoteApp.Controllers.ResetPassword;
using NoteApp.Models.ResetPassword;
using NoteApp.Utils;
using NoteApp.Views.Interfaces;
using Plugin.Firebase.CloudMessaging;
using System.Windows.Input;

namespace NoteApp.Views;

public partial class ResetPassword : ContentPage, ResetPasswordViewInterface
{
	private string _email;
    private ResetPasswordController _controller;
	public ResetPassword( string email)
	{
		InitializeComponent();
		this._email = email;
        _controller = new ResetPasswordController((ResetPasswordViewInterface)this);
    }


    private async void VerifyCode_Clicked(object sender, EventArgs e)
    {
        string pass = txtPasswordReset.Text;
        string pass2 = txtPasswordResetVer.Text;
        if (Validator.ValidateString(pass) && Validator.ValidateString(pass2) && pass == pass2)
        {
            ResetPasswordPOST body = new ResetPasswordPOST() { email = _email, passwd = pass};
            _controller.ChangePassword(body);
        }
        else
        {
            DisplayAlert("Recuperar Contraseña", "Las contraseñas no coinciden", "Aceptar");
        }
    }
    public async void ShowResponseResetPassword(string msg, bool status)
    {
        await DisplayAlert("Recuperar Contraseña", msg, "Aceptar");
        if (status) { 
            await Navigation.PopAsync();
        }
    }
}