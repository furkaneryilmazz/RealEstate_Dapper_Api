using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.CategoryDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();//bir tane istemci örneği oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7101/api/Categories");//isteği atacağımız adresimizin url'sini verdik
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var jsonData = JsonConvert.SerializeObject(createCategoryDto); //gelen veriyi json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8,"application/json"); //json veriyi stringContent'e çevirdik
            var responseMessage = await client.PostAsync("https://localhost:7101/api/Categories", stringContent); //post işlemi yaptık
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var responseMessage = await client.DeleteAsync($"https://localhost:7101/api/Categories/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var responseMessage = await client.GetAsync($"https://localhost:7101/api/Categories/{id}");//get işlemi yaptık
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();//gelen veriyi okuduk
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);//gelen veriyi UpdateCategoryDto'ya çevirdik
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto); //gelen veriyi json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //json veriyi stringContent'e çevirdik
            var responseMessage = await client.PutAsync("https://localhost:7101/api/Categories/", stringContent); //put işlemi yaptık 
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
