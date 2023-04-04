using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Weather_Web.Pages
{
    public class IndexModel : PageModel
    {
        public List<WeatherNews> WeatherNews { get; private set; }

        public IndexModel()
        {
        }

        public void OnGet()
        {
            WeatherNews = GetDummyWeatherNews();
        }

        private List<WeatherNews> GetDummyWeatherNews()
        {
            return new List<WeatherNews>
            {
                new WeatherNews
                {
                    Title = "Peristiwa Cuaca Ekstrem di Surabaya Terus Meningkat",
                    Summary = "Studi terbaru menunjukkan adanya peningkatan kejadian cuaca ekstrem...",
                    ImageUrl = "https://cdn.antaranews.com/cache/730x487/2021/08/12/22.jpg",
                    Date = DateTime.Now.AddDays(-1)
                },
                new WeatherNews
                {
                    Title = "Bersiap Menghadapi Musim Badai",
                    Summary = "Pelajari cara mempersiapkan rumah dan keluarga Anda untuk menghadapi musim badai yang akan datang...",
                    ImageUrl = "https://observerid.com/wp-content/uploads/2021/05/Nat-6.png",
                    Date = DateTime.Now.AddDays(-3)
                },
                new WeatherNews
                {
                    Title = "Kondisi Kekeringan Terus Berlanjut di Wilayah Barat",
                    Summary = "Kondisi kekeringan yang berkepanjangan di wilayah barat menyebabkan kekhawatiran...",
                    ImageUrl = "https://dpu.kulonprogokab.go.id/files/news/normal/840bf3c20e2df966773345213f8075e0.jpg",
                    Date = DateTime.Now.AddDays(-5)
                }
            };
        }
    }

    public class WeatherNews
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
    }
}
