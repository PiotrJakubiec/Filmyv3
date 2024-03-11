using Filmy.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Filmy.Controllers
{
    public class MovieController : Controller
    {

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            // Use _httpClient to make HTTP requests
            HttpResponseMessage response = await client.GetAsync("https://api.themoviedb.org/3/discover/movie?api_key=ab3bf8e762d05382cba6eeb892b43917&include_adult=false&include_video=false&language=pl-PL&page=1&sort_by=popularity.desc");

            if (response.IsSuccessStatusCode)
            {
                // Read the JSON content from the response
                var jsonContent = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON content into MovieResult object
                var movieResult = JsonConvert.DeserializeObject<Root>(jsonContent);


                
                ViewData["Rezultaty"] = movieResult;

                return View("Result");
            }
            else
            {
                return Content("Failed to fetch movie data from the API.");
            }
        }
    }
}