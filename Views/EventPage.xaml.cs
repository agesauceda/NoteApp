using NoteApp.Controllers.EventPage;
using NoteApp.Models.EventPage;
using NoteApp.Views.Interfaces;
using NoteApp.Utils;

namespace NoteApp.Views;

public partial class EventPage : ContentPage, EventPageViewInterface
{
	private EventPageController _controller;
	private ReminderPOST e;

	public EventPage()
	{
		InitializeComponent();
		this._controller = new EventPageController((EventPageViewInterface)this);
	}

 protected override async void OnAppearing()
    {
        base.OnAppearing();
    }

   
    private async Task getData() {

		try {
        string titulo = txtTitulo.Text;
        string descripcion = txtDescripcion.Text;
        string ubicacion = txtUbicacion.Text;
       // string imagen = txtImagen.Content;
        string fecha_inicio = txtFechaIni.Date.ToString("yyyy-MM-dd");
        string fecha_final= txtFechaFin.Date.ToString("yyyy-MM-dd");
		string hora_final = new DateTime(txtHoraFin.Time.Ticks).ToString("HH:mm:ss");
		string hora_inicio = new DateTime(txtHoraIni.Time.Ticks).ToString("HH:mm:ss");

        if(Validator.ValidateString(titulo) && Validator.ValidateString(descripcion) && Validator.ValidateString(ubicacion) 
            && Validator.ValidateString(fecha_inicio) && Validator.ValidateString(fecha_final))
        {
            e = new ReminderPOST
            {
                titulo = titulo,
                descripcion = descripcion,
                ubicacion = ubicacion,
              //  imagen = imagen,
                fecha_inicio = fecha_inicio + " " + hora_inicio,
                fecha_final = fecha_final + " " + hora_final,
               
            };

		
            await _controller.InsertReminder(e);
            flushData();
        }
        else
        {
            await DisplayAlert("Error", "Llene todos los campos", "OK");
        }
		} catch(Exception e) {
			await DisplayAlert("Excpcion", e.Message, "OK");
		}
    }
    private void flushData() {
        e = null;
   
		txtTitulo.Text = string.Empty;
		txtDescripcion.Text = string.Empty;
		txtUbicacion.Text = string.Empty;
		txtImagen.Content = null;
		txtFechaIni.Date = DateTime.Now; 
		txtFechaFin.Date = DateTime.Now;
        
    }

    public async Task InsertReminder(string msg)
    {
         await DisplayAlert("Success", msg, "OK");
    }

	private async void OnGuardarEvento_Clicked(object sender, EventArgs e)
	{
		await getData();
	}

}