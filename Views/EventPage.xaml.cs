using NoteApp.Controllers.EventPage;
using NoteApp.Models.EventPage;
using NoteApp.Views.Interfaces;
using NoteApp.Utils;
using CommunityToolkit.Maui.Media;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace NoteApp.Views;

public partial class EventPage : ContentPage, EventPageViewInterface
{
    private Pin pinLocation = null;
    private Location location;
    private EventPageController _controller;
    private ReminderPOST e;
    string fotoBase64;

    public EventPage()
    {
        InitializeComponent();
        this._controller = new EventPageController((EventPageViewInterface)this);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RequestPermissions.SolicitarPermisosUbicacion();
    }


    private async Task SetDefaultImageBase64()
    {
        try
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("noimage.jpg");

            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                byte[] defaultImageBytes = memoryStream.ToArray();
                this.fotoBase64 = Convert.ToBase64String(defaultImageBytes);
                imgPreview.Source = ImageSource.FromStream(() => new MemoryStream(defaultImageBytes));
            }
        }
        catch (Exception ex)
        {
           // await DisplayAlert("Error", $"No se pudo cargar la imagen predeterminada: {ex.Message}", "OK");
        }
    }

    private async Task getData()
    {
        try
        {
            string titulo = txtTitulo.Text;
            string descripcion = txtDescripcion.Text;
            string ubicacion = txtUbicacion.Text;
            string fecha_inicio = txtFechaIni.Date.ToString("yyyy-MM-dd");
            string fecha_final = txtFechaFin.Date.ToString("yyyy-MM-dd");
            string hora_final = new DateTime(txtHoraFin.Time.Ticks).ToString("HH:mm:ss");
            string hora_inicio = new DateTime(txtHoraIni.Time.Ticks).ToString("HH:mm:ss");

            if (Validator.ValidateString(titulo) && Validator.ValidateString(descripcion)
                && Validator.ValidateString(fecha_inicio) && Validator.ValidateString(fecha_final))
            {
                if (string.IsNullOrEmpty(ubicacion))
                {
                    await DisplayAlert("Error", "La ubicación no puede estar vacía.", "OK");
                    return;  
                }

                if (string.IsNullOrEmpty(this.fotoBase64))
                {
                    await SetDefaultImageBase64();
                }

                e = new ReminderPOST
                {
                    titulo = titulo,
                    descripcion = descripcion,
                    ubicacion = ubicacion,  
                    imagen = this.fotoBase64,
                    fecha_inicio = fecha_inicio + " " + hora_inicio,
                    fecha_final = fecha_final + " " + hora_final,
                };

                if (string.IsNullOrEmpty(this.fotoBase64))
                {
                    await DisplayAlert("Recordatorio", "No se ha cargado ninguna imagen.", "OK");
                }
                else
                {
                    Console.WriteLine("Base64 Enviado: " + this.fotoBase64.Substring(0, 100));
                }

                await _controller.InsertReminder(e);
                flushData();
            }
            else
            {
                await DisplayAlert("Error", "Llene todos los campos", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Excepción", ex.Message, "OK");
        }
    }

    private void flushData()
    {
        e = null;

        txtTitulo.Text = string.Empty;
        txtDescripcion.Text = string.Empty;
        txtUbicacion.Text = string.Empty;
        imgPreview.Source = null;
        txtFechaIni.Date = DateTime.Now;
        txtFechaFin.Date = DateTime.Now;
        if (pinLocation != null)
        {
            MapLoc.Pins.Remove(pinLocation);
            pinLocation = null;  
        }
        MapLoc.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(15.848705, -87.934471), Distance.FromKilometers(1)));

    }

    public async Task InsertReminder(string msg)
    {
        await DisplayAlert("Success", msg, "OK");
    }

    private async void OnGuardarEvento_Clicked(object sender, EventArgs e)
    {
        await getData();
    }

    private async void OnTakePhotoButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var foto = await MediaPicker.CapturePhotoAsync();

            if (foto != null)
            {
                var filePath = foto.FullPath;
                var fotoBase64 = await Converters.ConvertToBase64(filePath);

                if (!string.IsNullOrEmpty(fotoBase64))
                {
                    this.fotoBase64 = fotoBase64;
                    var imageSource = ImageSource.FromFile(filePath);
                    imgPreview.Source = imageSource;

                    await DisplayAlert("Success", "Foto tomada correctamente", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo convertir la imagen a Base64", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo capturar la foto: {ex.Message}", "OK");
        }
    }


    public async Task UpdateReminder(ReminderPUT reminder)
    {
        await DisplayAlert("Actualizacion de Evento", "aasdfasdf", "Aceptar");
    }

    public Task GetReminder(ReminderGET? reminder)
    {
        throw new NotImplementedException();
    }

    private void OnMapClicked(object sender, MapClickedEventArgs e) {
        location = e.Location;
        if (pinLocation != null) {
            MapLoc.Pins.Remove(pinLocation);
        }
        pinLocation = new Pin()
        {
            Label = "Lugar Seleccionado",
            Type = PinType.Place,
            Location = location
        };
        MapLoc.Pins.Add(pinLocation);
        txtUbicacion.Text = $"{location.Latitude},{location.Longitude}";

    }
}