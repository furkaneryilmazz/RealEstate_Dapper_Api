using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.BottomGridDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class BottomGridController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BottomGridController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();//bir tane istemci örneği oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7101/api/BottomGrids");//isteği atacağımız adresimizin url'sini verdik
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBottomGridDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateBottomGrid()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBottomGrid(CreateBottomGridDto createBottomGridDto)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var jsonData = JsonConvert.SerializeObject(createBottomGridDto); //gelen veriyi json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //json veriyi stringContent'e çevirdik
            var responseMessage = await client.PostAsync("https://localhost:7101/api/BottomGrids", stringContent); //post işlemi yaptık
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteBottomGrid(int id)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var responseMessage = await client.DeleteAsync($"https://localhost:7101/api/BottomGrids/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBottomGrid(int id)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var responseMessage = await client.GetAsync($"https://localhost:7101/api/BottomGrids/{id}");//get işlemi yaptık
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();//gelen veriyi okuduk
                var values = JsonConvert.DeserializeObject<UpdateBottomGridDto>(jsonData);//gelen veriyi UpdateCategoryDto'ya çevirdik
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBottomGrid(UpdateBottomGridDto updateBottomGridDto)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var jsonData = JsonConvert.SerializeObject(updateBottomGridDto); //gelen veriyi json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //json veriyi stringContent'e çevirdik
            var responseMessage = await client.PutAsync("https://localhost:7101/api/BottomGrids/", stringContent); //put işlemi yaptık 
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
