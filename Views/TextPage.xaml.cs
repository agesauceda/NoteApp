using NoteApp.Controllers.NotePage;
using NoteApp.Models.TextPage;
using NoteApp.Views.Interfaces;
using System.Diagnostics;

namespace NoteApp.Views;

public partial class TextPage : ContentPage, TextPageViewInterface
{
    private readonly TextPageController _controller;

    public TextPage()
    {
        InitializeComponent();
        _controller = new TextPageController(this); 
    }

    private async Task GetDataAndCreateNote()
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
            
            await _controller.CreateNote(note); 
        }
        else
        {
            await DisplayAlert("Error", "El título y la descripción son obligatorios.", "OK");
        }
    }

    private async void SaveNoteButton_Clicked(object sender, EventArgs e)
    {
        Console.WriteLine($"campos{txtTitle.Text}");
        Console.WriteLine($"campos{txtDescription.Text}");
        await GetDataAndCreateNote();
       
    }

    public async Task CreateNote(string msg)
    {
        await DisplayAlert("Resultado", msg, "OK");
        // ClearFields(); 
    }

    public Task UpdateNote(string msg)
    {
        throw new NotImplementedException();
    }

    public Task LoadNoteForEdit(NoteTextPOST note)
    {
        throw new NotImplementedException();
    }
    //para limpiar
    //private void ClearFields()
    //{
    //    txtTitle.Text = string.Empty;
    //    txtDescription.Text = string.Empty;
    //}


}
