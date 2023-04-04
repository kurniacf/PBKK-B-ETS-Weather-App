using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace WeatherWeb.Pages
{
    public class WeatherModel : PageModel
    {
        public string? City { get; private set; }
        public string? Country { get; private set; }
        public string? Temperature { get; private set; }
        public string? WeatherDescription { get; private set; }
        public string? TempMin { get; private set; }
        public string? TempMax { get; private set; }
        public string? Humidity { get; private set; }
        public string? Pressure { get; private set; }
        public string? WindSpeed { get; private set; }
        public string? WindDirection { get; private set; }
        public string? Visibility { get; private set; }
        public string? Sunrise { get; private set; }
        public string? Sunset { get; private set; }

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherModel(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<IActionResult> OnPostAsync(string city, string country)
        {
            City = city;
            Country = country;

            try
            {
                var apiKey = _configuration["OpenWeatherMap:ApiKey"];
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={City},{Country}&appid={apiKey}&units=metric";

                Console.WriteLine($"Requesting weather data: {url}");
                var response = await _httpClient.GetFromJsonAsync<WeatherResponse>(url);

                if (response != null)
                {
                    Temperature = $"{response.Main.Temp}°C";
                    TempMin = $"{response.Main.Temp_min}°C";
                    TempMax = $"{response.Main.Temp_max}°C";
                    Humidity = $"{response.Main.Humidity}%";
                    Pressure = $"{response.Main.Pressure} hPa";
                    WindSpeed = $"{response.Wind.Speed} m/s";
                    WindDirection = $"{response.Wind.Deg}°";
                    WeatherDescription = response.Weather.FirstOrDefault()?.Description;
                    Visibility = $"{response.Visibility / 1000.0} km";
                    Sunrise = DateTimeOffset.FromUnixTimeSeconds(response.Sys.Sunrise).ToLocalTime().ToString("HH:mm");
                    Sunset = DateTimeOffset.FromUnixTimeSeconds(response.Sys.Sunset).ToLocalTime().ToString("HH:mm");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to get weather data.");
                    Console.WriteLine("Failed to get weather data.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                Console.WriteLine($"Error: {ex.Message}");
            }

            return Page();
        }

        private class WeatherResponse
        {
            public MainWeatherData Main { get; set; } = new MainWeatherData();
            public WindData Wind { get; set; } = new WindData();
            public List<WeatherData> Weather { get; set; } = new List<WeatherData>();
            public int Visibility { get; set; }
            public SysData Sys { get; set; } = new SysData();
        }

        private class WeatherData
        {
            public string Description { get; set; } = string.Empty;
        }

        private class MainWeatherData
        {
            public float Temp
            {
                get; set;
            }
            public float Temp_min { get; set; }
            public float Temp_max { get; set; }
            public int Humidity { get; set; }
            public int Pressure { get; set; }
        }

        private class WindData
        {
            public float Speed { get; set; }
            public int Deg { get; set; }
        }

        private class SysData
        {
            public long Sunrise { get; set; }
            public long Sunset { get; set; }
        }
    }
}

