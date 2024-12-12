using NoteApp.Controllers.NotePage;
using NoteApp.Models.Dashboard;
using NoteApp.Models.TextPage;
using NoteApp.Views.Interfaces;
using System.Diagnostics;

namespace NoteApp.Views;

public partial class TextPageUpdate : ContentPage, TextPageViewInterface
{
    private readonly TextPageController _controller;
    //private readonly int _noteId;
    private NoteTextPOST noteText = new NoteTextPOST();
    private ObjectDashBoard _noteId;

    public TextPageUpdate(ObjectDashBoard id)
    {
        InitializeComponent();

        _controller = new TextPageController(this);
        _noteId = id;

        noteText.id = id.id;
        noteText.titulo = id.titulo;
        noteText.descripcion = id.contenido;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        txtTitle.Text = noteText.titulo;
        txtDescription.Text = noteText.descripcion;
    }

    private async Task GetDataAndUpdateNote()
    {
        string? title = txtTitle.Text;
        string? description = txtDescription.Text;

        if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(description))
        {
            NoteTextPOST note = new NoteTextPOST
            {
                titulo = title,
                descripcion = description
            };

            await _controller.UpdateNote(_noteId.id.Value, note); 
        }
        else
        {
            await DisplayAlert("Error", "El título y la descripción son obligatorios.", "OK");
        }
    }

    private async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        await GetDataAndUpdateNote();
    }

    public async Task UpdateNote(string msg)
    {
        await DisplayAlert("Resultado", msg, "OK");
        await Navigation.PopAsync(); 
    }

    public Task CreateNote(string msg)
    {
        throw new NotImplementedException();
    }
}
