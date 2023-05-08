using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnweWeatherApp
{

    public class ForecastPageModel
    {

        public List<WeatherData> WeatherDataRecords { get; }

        public double TempMin { get
            {
                if (WeatherDataRecords.Count == 0)
                {
                    return 0.0;
                }

                return WeatherDataRecords.Min(x => x.Main.TempMin);
            }
        }

        public double TempMax
        {
            get
            {
                if (WeatherDataRecords.Count == 0)
                {
                    return 0.0;
                }

                return WeatherDataRecords.Max(x => x.Main.TempMax);
            }
        }

        public string  Location { get; set; }
        public long Sunrise { get; set; }
        public long Sunset { get; set; }
        public long DayDuration
        {
            get
            {
                return Sunset - Sunrise;
            }
        }
        public double FeelsLike { get; set; }

        public string CurrentDate { get; set; }


        public ForecastPageModel(WeatherDataExtended weatherData, int day)
        {
            WeatherDataRecords = _getDateWeather(weatherData, _gePropertDate(weatherData, day));
            Location = weatherData.City.Name;
            Sunrise = weatherData.City.Sunrise;
            Sunset = weatherData.City.Sunset;
            CurrentDate = weatherData.WeatherDataRecords[0].DateString.Split(' ')[0];
        }

        internal string _gePropertDate(WeatherDataExtended weatherData, int day)
        {
            string dateString = weatherData.WeatherDataRecords[0].DateString;
            dateString = dateString.Split(' ')[0];
            int date = int.Parse(dateString.Substring(dateString.Length - 2)) + day;

            string result =  dateString.Substring(0, dateString.Length - 2);
            if (date > 9)
            {
                return  result + (date).ToString();
            } else
            {
                return  result + '0' + (date).ToString();
            }
        }

        internal List<WeatherData> _getDateWeather(WeatherDataExtended weatherData, string date)
        {
            return weatherData.WeatherDataRecords.Where(x => x.DateString.Substring(0, 10).Equals(date)).ToList();
        }

    }
}
