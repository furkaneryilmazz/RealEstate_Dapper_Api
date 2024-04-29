using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.EmployeeDtos;
using RealEstate_Dapper_UI.Dtos.WhoWeAreDtos;
using System.Text;

namespace RealEstate_Dapper_UI.Controllers
{
    public class WhoWeAreController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WhoWeAreController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();//bir tane istemci örneği oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7101/api/WhoWeAreDetail");//isteği atacağımız adresimizin url'sini verdik
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateWhoWeAreDetail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWhoWeAreDetail(CreateWhoWeAreDetailDto createWhoWeAreDetailDto)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var jsonData = JsonConvert.SerializeObject(createWhoWeAreDetailDto); //gelen veriyi json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //json veriyi stringContent'e çevirdik
            var responseMessage = await client.PostAsync("https://localhost:7101/api/WhoWeAreDetail", stringContent); //post işlemi yaptık
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteWhoWeAreDetail(int id)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var responseMessage = await client.DeleteAsync($"https://localhost:7101/api/WhoWeAreDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateWhoWeAreDetail(int id)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var responseMessage = await client.GetAsync($"https://localhost:7101/api/WhoWeAreDetail/{id}");//get işlemi yaptık
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();//gelen veriyi okuduk
                var values = JsonConvert.DeserializeObject<UpdateWhoWeAreDetailDto>(jsonData);//gelen veriyi UpdateCategoryDto'ya çevirdik
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWhoWeAreDetail(UpdateWhoWeAreDetailDto updateWhoWeAreDetailDto)
        {
            var client = _httpClientFactory.CreateClient(); //bir tane istemci örneği oluşturduk
            var jsonData = JsonConvert.SerializeObject(updateWhoWeAreDetailDto); //gelen veriyi json'a çevirdik
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); //json veriyi stringContent'e çevirdik
            var responseMessage = await client.PutAsync("https://localhost:7101/api/WhoWeAreDetail/", stringContent); //put işlemi yaptık 
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
