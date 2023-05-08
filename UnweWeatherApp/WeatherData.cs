using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Newtonsoft.Json;
using Xamarin.Forms;

namespace UnweWeatherApp
{
    public class WeatherData
    {
        [JsonProperty("name")]
        public string Title { get; set; }
        [JsonProperty("coord")]
        public Coord Coord { get; set; }
        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("visibility")]
        public long Visibility { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        [JsonProperty("dt")]
        public long Dt { get; set; }
        [JsonProperty("sys")]
        public Sys Sys { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("cod")]
        public long Cod { get; set; }
        [JsonProperty("dt_txt")]
        public string DateString { get; set; }

        public string CurrentDate
        {
            get
            {
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Dt).ToLocalTime();
                return dt.ToString("dd-MM-yyyy");
            }
        }
    }
    public class Clouds
    {
        [JsonProperty("all")]
        public long All { get; set; }
    }
    public class Coord
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }
        [JsonProperty("lat")]
        public double Lat { get; set; }
    }
    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        [JsonProperty("pressure")]
        public long Pressure { get; set; }
        [JsonProperty("humidity")]
        public long Humidity { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }
    }
    public class Sys
    {
        [JsonProperty("type")]
        public long Type { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("message")]
        public double Message { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }
        [JsonProperty("sunset")]
        public long Sunset { get; set; }

    }
    public class Weather
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("main")]
        public string Visibility { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        public string IconURL
        {
            get
            {
                return $"https://openweathermap.org/img/wn/{Icon}.png";
            }
        }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
        [JsonProperty("deg")]
        public long Deg { get; set; }
    }

    public class WeatherDataExtended
    {
        [JsonProperty("list")]
        public List<WeatherData> WeatherDataRecords { get; set; }
        [JsonProperty("city")]
        public City City { get; set; }
    }

    public class City
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }

        public long DayDuration
        {
            get
            {
                return Sunset - Sunrise;
            }
        }
    }

    class DayDurationTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan span = TimeSpan.FromSeconds((long)value);
            string timeStr = string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);
            return timeStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}