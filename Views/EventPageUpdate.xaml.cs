using NoteApp.Controllers.EventPage;
using NoteApp.Models.EventPage;
using NoteApp.Views.Interfaces;
using NoteApp.Utils;
using CommunityToolkit.Maui.Media;
using NoteApp.Models.Dashboard;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
//using AndroidX.Window.Embedding;

namespace NoteApp.Views;

public partial class EventPageUpdate : ContentPage, EventPageViewInterface
{
    private EventPageController _controller;
    private ReminderPUT e;
    private Pin pinLocation = null;
    private Location location;
    string fotoBase64;

    private ObjectDashBoard obj;
    int id = 0;

    public EventPageUpdate(ObjectDashBoard o)
    {
        InitializeComponent();
        this.id = (int)(o.id != null ? o.id : 0);
        obj = o;
        this._controller = new EventPageController((EventPageViewInterface)this);

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _controller.GetReminder(obj.id.Value);
        await RequestPermissions.SolicitarPermisosUbicacion();
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

            if (Validator.ValidateString(titulo) && Validator.ValidateString(descripcion) && Validator.ValidateString(ubicacion)
                && Validator.ValidateString(fecha_inicio) && Validator.ValidateString(fecha_final))
            {
                this.e = new ReminderPUT
                {
                    id = this.id,
                    titulo = titulo,
                    descripcion = descripcion,
                    ubicacion = string.IsNullOrEmpty(ubicacion) ? null : ubicacion,
                    imagen = string.IsNullOrEmpty(this.fotoBase64) ? null : this.fotoBase64,
                    fecha_inicio = fecha_inicio + " " + hora_inicio,
                    fecha_final = fecha_final + " " + hora_final,

                };

                if (string.IsNullOrEmpty(this.fotoBase64))
                {
                    await DisplayAlert("Error", "No se ha cargado ninguna imagen.", "OK");


                }
                else
                {
                    Console.WriteLine("Base64 Enviado: " + this.fotoBase64.Substring(0, 100));

                }

                await _controller.UpdateReminder(e, id);
                flushData();
            }
            else
            {
                await DisplayAlert("Error", "Llene todos los campos", "OK");
            }
        }
        catch (Exception e)
        {
            await DisplayAlert("Excpcion", e.Message, "OK");
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


    private async void OnActualizarEvento_Clicked(object sender, EventArgs e)
    {
        await getData();

    }

    private void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        location = e.Location;
        if (pinLocation != null)
        {
            MapLoc.Pins.Remove(pinLocation);
        }
        pinLocation = new Pin()
        {
            Label = "Lugar Seleccionado",
            Type = PinType.Place,
            Location = location
        };
        MapLoc.Pins.Add(pinLocation);
        txtUbicacion.Text = $"{location.Latitude}, {location.Longitude}";

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

    public Task InsertReminder(string msg)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateReminder(ReminderPUT reminder)
    {
        await DisplayAlert("Éxito", "Recordatorio actualizado correctamente.", "OK");
    }

    public async Task GetReminder(ReminderGET? reminder)
    {
        if (reminder != null)
        {
            txtTitulo.Text = reminder.titulo;
            txtDescripcion.Text = reminder.descripcion;
            txtUbicacion.Text = reminder.ubicacion ?? string.Empty;
            string[] arr = reminder.ubicacion.Split(",");
            location = new Location()
            {
                Latitude = Convert.ToDouble(arr[0]),
                Longitude = Convert.ToDouble(arr[1]),
            };
            pinLocation = new Pin()
            {
                Label = "Lugar Seleccionado",
                Type = PinType.Place,
                Location = location
            };
            MapLoc.Pins.Add(pinLocation);
            MapSpan moveMap = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(0.5));
            MapLoc.MoveToRegion(moveMap);


            txtFechaIni.Date = DateTimeOffset.Parse(reminder.fecha_inicio).DateTime;
            txtFechaFin.Date = DateTimeOffset.Parse(reminder.fecha_final).DateTime;
            txtHoraIni.Time = DateTimeOffset.Parse(reminder.fecha_inicio).TimeOfDay;
            txtHoraFin.Time = DateTimeOffset.Parse(reminder.fecha_final).TimeOfDay;

            if (!string.IsNullOrEmpty(reminder.imagen))
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(reminder.imagen);
                    imgPreview.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    fotoBase64 = reminder.imagen; 
                }
                catch (FormatException ex)
                {       
                    await DisplayAlert("Error", "La imagen no tiene un formato Base64 válido.", "OK");
                }
                }
                else
                {
                    imgPreview.Source = null; 
                    fotoBase64 = null; 
                }
            }
            else
            { await DisplayAlert("Error", "No se ha recibido imagen en el recordatorio.", "OK"); }
        }
    }
