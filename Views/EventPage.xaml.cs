using Microsoft.Maui.Controls.Maps;
using NoteApp.Utils;

namespace NoteApp.Views;
public partial class EventPage : ContentPage
{
    private Pin pinLocation = null;
    private Location location;
	public EventPage()
	{
		InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
		await RequestPermissions.SolicitarPermisosUbicacion();
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
    }
}