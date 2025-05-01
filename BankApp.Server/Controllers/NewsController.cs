using BankApp.Server.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        [HttpGet("news")]
        public IActionResult News() {

            var news = new NewsDTO { Title = "Nowosci", Content = "Nasz bank jest najlepszy", Date = DateOnly.FromDateTime(DateTime.Now) };

            var news1 = new NewsDTO { Title = "Nowosci", Content = "Nasz bank jest najlepszy", Date = DateOnly.FromDateTime(DateTime.Now) };

            var news2 = new NewsDTO { Title = "Nowosci", Content = "Nasz bank jest najlepszy", Date = DateOnly.FromDateTime(DateTime.Now) };

            var newsList = new List<NewsDTO>();
            newsList.Add(news);
            newsList.Add(news2);
            Console.WriteLine(news);
            return Ok(newsList);
        }
    }
}
