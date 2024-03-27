using Microsoft.Maui.Maps;

namespace Lab1;

using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

public partial class MainPage : ContentPage
{
    private const string WaterMark1 = "Burtik Lobarev";
    private const string AlertTitle = "Burtik Lobarev says";
    private List<Pin> _savedPins = new List<Pin>();
    private Pin _selectedPin;
    private Pin _latestPin;
    public MainPage()
    {
        InitializeComponent();
        WaterMarkLabel.Text = WaterMark1;
        MapTypePicker.SelectedIndex = 0;
        LoadPins();
        LoadDataPicker();
        MapSpan span = new MapSpan(new Location(52.1, 23.76), 0.02, 0.05);
        GMap.MoveToRegion(span);
    }

    private void LoadPins()
    {
        Pin pin1 = new Pin
        {
            Label = "Test1",
            Address = $"{51.5}, {26.5}",
            Type = PinType.Place,
            Location = new Location(51.5, 26.5)
        };
        Pin pin2 = new Pin
        {
            Label = "Test2",
            Address = $"{0}, {0}",
            Type = PinType.Place,
            Location = new Location(0, 0)
        };
        _savedPins.Add(pin1);
        _savedPins.Add(pin2);
    }

    private void LoadDataPicker()
    {
        List<string> pins = new List<string>();
        foreach (var pin in _savedPins)
        {
            pins.Add(pin.Label);
        }
        PinsPicker.ItemsSource = pins;
        

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

        GMap.Pins.Remove(_latestPin);

        Pin pin = new Pin
        {
            Label = AlertTitle,
            Address = $"{latitude}, {longitude}",
            Type = PinType.Place,
            Location = new Location(latitude, longitude)
        };
        GMap.Pins.Add(pin);
        _latestPin = pin;
    }

    

    private void PinsPicker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        _selectedPin = _savedPins[PinsPicker.SelectedIndex];
        if (GMap.Pins.Contains(_selectedPin))
        {
            HideCheckBox.IsChecked = true;
        }
        else
        {
            HideCheckBox.IsChecked = false;
        }
        PinGrid.IsVisible = true;
    }

    private void HideCheckBox_OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        if (!HideCheckBox.IsChecked)
        {
            GMap.Pins.Remove(_selectedPin);
        }
        else
        {
            GMap.Pins.Add(_selectedPin);
        }
    }

    private void NavigateButton_OnClicked(object? sender, EventArgs e)
    {
        GMap.MoveToRegion(new MapSpan(_selectedPin.Location, 0.02, 0.05));
    }

    private void DeletePinButton_OnClicked(object? sender, EventArgs e)
    {
        GMap.Pins.Remove(_selectedPin);
        _savedPins.Remove(_selectedPin);
        if (_savedPins.Count != 0)
        {
            _selectedPin = _savedPins[0];
        }
        else
        {
            PinGrid.IsVisible = false;
        }
       // LoadDataPicker();
        
    }
}
