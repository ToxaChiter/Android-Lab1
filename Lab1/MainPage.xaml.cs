using Microsoft.Maui.Maps;

namespace Lab1;

using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

public partial class MainPage : ContentPage
{
    private const string WaterMark1 = "Butik Lobarev";
    private const string AlertTitle = "Butik Lobarev says";
    public MainPage()
    {
        InitializeComponent();
        WaterMarkLabel.Text = WaterMark1;
    }

    private void PickerSelectedIndexChanged(object? sender, EventArgs e)
    {
        switch (MapTypePicker.SelectedIndex)
        {
            case 0:
                GMap.MapType = MapType.Street;
                break;
            case 1:
                GMap.MapType = MapType.Satellite;
                break;
            case 2:
                GMap.MapType = MapType.Hybrid;
                break;
        }


    }

    private void TrafficSwitch_OnToggled(object? sender, ToggledEventArgs e)
    {
        GMap.IsTrafficEnabled = TrafficSwitch.IsToggled;
    }
    void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        (double latitude, double longitude) = (e.Location.Latitude, e.Location.Longitude);

        GMap.Pins.Clear();

        Pin pin = new Pin
        {
            Label = "Anton Lobarev",
            Address = $"{latitude}, {longitude}",
            Type = PinType.Place,
            Location = new Location(latitude, longitude)
        };
        GMap.Pins.Add(pin);
    }
}
