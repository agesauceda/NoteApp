using NoteApp.Controllers.EventPage;
using NoteApp.Models.EventPage;
using NoteApp.Views.Interfaces;
using NoteApp.Utils;
using CommunityToolkit.Maui.Media;
using NoteApp.Models.Dashboard;
//using AndroidX.Window.Embedding;

namespace NoteApp.Views;

public partial class EventPageUpdate : ContentPage, EventPageViewInterface
{
    private EventPageController _controller;
    private ReminderPUT e;
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
         txtTitulo.Text = obj.titulo;
         txtDescripcion.Text = obj.contenido;
         txtFechaIni.Date = DateTime.Parse(obj.fechaInicio);
         txtFechaFin.Date = DateTime.Parse(obj.fechaFinal);
         //txtHoraIni.Time = DateTime.Parse(obj.fechaInicio, null, System.Globalization.DateTimeStyles.RoundtripKind).TimeOfDay;
         await _controller.GetReminder(obj.id.Value);
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
                    ubicacion = ubicacion,
                    imagen = this.fotoBase64,
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

    }
  

    private async void OnActualizarEvento_Clicked(object sender, EventArgs e)
    {
        await getData();
       
    }

    private async void OnTakePhotoButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var res = await RequestPermissions.CheckPermissionCameraAsync();
            if (res != null)
            {
                var foto = await MediaPicker.CapturePhotoAsync();

                if (foto != null)
                {
                    using (var stream = await foto.OpenReadAsync())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            byte[] fotoBytes = memoryStream.ToArray();

                            string fotoBase64 = Convert.ToBase64String(fotoBytes);

                            this.fotoBase64 = fotoBase64;
                            var imageSource = ImageSource.FromStream(() => new MemoryStream(fotoBytes));
                            imgPreview.Source = imageSource;
                            await DisplayAlert("Success", "Foto tomada correctamente", "OK");
                        }
                    }
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
        await DisplayAlert("Éxito", "Recordatorio actualizado correctamente.", "OK");
    }
}