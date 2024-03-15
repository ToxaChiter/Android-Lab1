namespace Lab1;

using Microsoft.Maui.Controls.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    async void OnMapClicked(object sender, MapClickedEventArgs e)
    {
        await DisplayAlert("Lobarev says", $"Latitude: {e.Location.Latitude}, Longitude: {e.Location.Longitude}", "OK");
    }
}
