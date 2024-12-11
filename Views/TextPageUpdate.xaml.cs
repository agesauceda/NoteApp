using NoteApp.Controllers.NotePage;
using NoteApp.Models.TextPage;
using NoteApp.Views.Interfaces;
using System.Diagnostics;

namespace NoteApp.Views;

public partial class TextPageUpdate : ContentPage, TextPageViewInterface
{
    private readonly TextPageController _controller;
    private readonly int _noteId;

    public TextPageUpdate(int noteId)
    {
        InitializeComponent();
        _controller = new TextPageController(this);
        _noteId = noteId;

        // Cargar los datos de la nota al inicializar la vista.
        LoadNoteForEdit();
    }

    private async void LoadNoteForEdit()
    {
        try
        {
            NoteTextPOST? note = await _controller.GetNoteForEdit(_noteId); 
            if (note != null)
            {
                txtTitle.Text = note.titulo;
                txtDescription.Text = note.descripcion;
            }
            else
            {
                await DisplayAlert("Error", "No se pudo cargar la nota seleccionada.", "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error al cargar la nota: {ex.Message}");
            await DisplayAlert("Error", "Ocurrió un problema al cargar los datos.", "OK");
        }
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

            await _controller.UpdateNote(_noteId, note); // Se actualiza la nota usando el controlador.
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
        await Navigation.PopAsync(); // Regresar a la pantalla anterior tras actualizar.
    }

    public Task CreateNote(string msg)
    {
        throw new NotImplementedException();
    }
}
