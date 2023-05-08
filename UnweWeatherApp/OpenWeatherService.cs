using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace UnweWeatherApp
{
    public class OpenWeatherService
    {
        HttpClient _client;
        public OpenWeatherService()
        {
            _client = new HttpClient();
        }
        public async Task<WeatherDataExtended> GetWeatherData(string query)
        {
            WeatherDataExtended weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherDataExtended>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }
            return weatherData;
        }
    }
}
