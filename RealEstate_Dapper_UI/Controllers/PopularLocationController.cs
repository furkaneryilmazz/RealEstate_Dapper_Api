using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.PopularLocationDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class PopularLocationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PopularLocationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();//bir tane istemci örneği oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7101/api/PopularLocations");//isteği atacağımız adresimizin url'sini verdik
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultPopularLocationDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreatePopularLocation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePopularLocation(CreatePopularLocationDto createPopularLocationDto)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var jsonData = JsonConvert.SerializeObject(createPopularLocationDto); //gelen veriyi json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //json veriyi stringContent'e çevirdik
            var responseMessage = await client.PostAsync("https://localhost:7101/api/PopularLocations", stringContent); //post işlemi yaptık
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeletePopularLocation(int id)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var responseMessage = await client.DeleteAsync($"https://localhost:7101/api/PopularLocations/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdatePopularLocation(int id)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var responseMessage = await client.GetAsync($"https://localhost:7101/api/PopularLocations/{id}");//get işlemi yaptık
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();//gelen veriyi okuduk
                var values = JsonConvert.DeserializeObject<UpdatePopularLocationDto>(jsonData);//gelen veriyi UpdateCategoryDto'ya çevirdik
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePopularLocation(UpdatePopularLocationDto updatePopularLocationDto)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var jsonData = JsonConvert.SerializeObject(updatePopularLocationDto); //gelen veriyi json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //json veriyi stringContent'e çevirdik
            var responseMessage = await client.PutAsync("https://localhost:7101/api/PopularLocations/", stringContent); //put işlemi yaptık 
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
