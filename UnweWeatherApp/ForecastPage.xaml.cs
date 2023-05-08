using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnweWeatherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastPage : ContentPage
    {
        OpenWeatherService _openWeatherService;
        int _day;

        public ForecastPage(int day)
        {
            _openWeatherService = new OpenWeatherService();
            _day = day;

            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                DeviceLabel.Text = "Android";
            }
            else
            {
                DeviceLabel.Text = "IOS";

            }

            GetWeatherWithGeoLoaction();

        }

        public string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&units=metric"; // or units=metric
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

        string GenerateRequestUriGeo(string endpoint, double lat, double longt)
        {
            string requestUri = endpoint;
            requestUri += $"?lat={lat}";
            requestUri += $"&lon={longt}";
            requestUri += "&units=metric"; // or units=metric
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

            public async void GetWeatherWithGeoLoaction()
        {
            var location = await Geolocation.GetLocationAsync();
            double lat = 0;
            double lon = 0;
            if (location != null)
            {
                lat = location.Latitude;
                lon = location.Longitude;
                WeatherDataExtended weatherData = await _openWeatherService.GetWeatherData(GenerateRequestUriGeo(Constants.OpenWeatherMapEndpoint, lat, lon));

                ForecastPageModel data = new ForecastPageModel(weatherData, _day);
                BindingContext = data;
            }
        }

        async public void OnGetWeatherButtonClicked(object x, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherDataExtended weatherData = await _openWeatherService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));


                ForecastPageModel data = new ForecastPageModel(weatherData, _day);
                BindingContext = data;
            }

            StartTorchAndVibrate();
        }

        private async void StartTorchAndVibrate()
        {
            // Start the torch
            try
            {
                // await Flashlight.TurnOnAsync();
            }
            catch (FeatureNotSupportedException)
            {
                // Handle the exception if the torch is not supported on the device
            }

            // Vibrate three times
            for (int i = 0; i < 3; i++)
            {
                Vibration.Vibrate(TimeSpan.FromMilliseconds(200));
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            }

            // Turn off the torch
            try
            {
                // await Flashlight.TurnOffAsync();
            }
            catch (FeatureNotSupportedException)
            {
                // Handle the exception if the torch is not supported on the device
            }
        }
    }
}