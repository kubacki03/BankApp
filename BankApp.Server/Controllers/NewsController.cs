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

            var news = new NewsDTO { Title = "Prosta i użyteczna – poznaj nową stronę www", Content = "Od zawsze proponujemy rozwiązania technologiczne, które są proste i intuicyjne. Idea „technologii do usług” była też kluczowa podczas projektowania naszej nowej strony www. Tak, nadszedł czas na kolejne zmiany, a dotyczą one właśnie naszego serwisu informacyjnego. W skrócie – strona będzie szybciej się ładować dzięki temu, że jest zbudowana w chmurze. Uproszczone treści pozwolą wam łatwiej znaleźć informacje.", Date = DateOnly.FromDateTime(DateTime.Now) };

            var news1 = new NewsDTO { Title = "Zrównoważony rozwój w produktach i usługach inwestycyjnych w BPL", Content = "Kwestie zrównoważonego rozwoju są dla nas niezwykle ważne i stanowią jeden z kluczowych elementów naszej działalności. Nieustannie wprowadzamy innowacje i rozwijamy się w sposób odpowiedzialny środowiskowo i społecznie.", Date = DateOnly.FromDateTime(DateTime.Now) };

            var news2 = new NewsDTO { Title = "Polski jazz fajny jest i ma swoją scenę na OFF Festivalu!", Content = "„Z dumą mogę ogłosić, że podczas tegorocznej edycji OFF Festivalu zagra nowa scena, w pełni poświęcona polskiej muzyce jazzowej – mBank OFF Jazz Club, na której pojawią się już uznani i najciekawsi nowi artyści” – zapowiada dyrektor artystyczny Artur Rojek. „Na OFF Festival Katowice przyjadą polscy muzycy, grający jazz na świetnym, światowym poziomie. I robiący to na własnych zasadach. ", Date = DateOnly.FromDateTime(DateTime.Now) };

            //var news3 = new NewsDTO { Title = "Nowosci", Content = "Nasz bank jest najlepszy", Date = DateOnly.FromDateTime(DateTime.Now) };

            //var news4 = new NewsDTO { Title = "Nowosci", Content = "Nasz bank jest najlepszy", Date = DateOnly.FromDateTime(DateTime.Now) };

            //var news5 = new NewsDTO { Title = "Nowosci", Content = "Nasz bank jest najlepszy", Date = DateOnly.FromDateTime(DateTime.Now) };

            var newsList = new List<NewsDTO> { news,news1,news2};
         
            Console.WriteLine(news);
            return Ok(newsList);
        }
    }
}
