using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_Api.UI.ProductDetailDtos;
using RealEstate_Dapper_Api.UI.ProductDtos;
using RealEstate_Dapper_UI.Dtos.ProductDtos;

namespace RealEstate_Dapper_UI.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PropertyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();//bir tane istemci örneği oluşturduk
            var responseMessage = await client.GetAsync("https://localhost:7101/api/Products/ProductListWithCategory");//isteği atacağımız adresimizin url'sini verdik
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PropertySingle(int id)
        {
            id =1;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7101/api/Products/GetProductByProductId?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync(); //json veriyi stringe çevirdik
            var values = JsonConvert.DeserializeObject<GetProductByProductIdDto>(jsonData); //string veriyi nesneye çevirdik

            var responseMessage2 = await client.GetAsync("https://localhost:7101/api/ProductDetails/GetProductDetailByProductId?id=" + id);
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync(); //json veriyi stringe çevirdik
            var values2 = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData2); //string veriyi nesneye çevirdik

            ViewBag.Tittle = values.Tittle.ToString();
            ViewBag.Price = values.Price;
            ViewBag.City = values.City;
            ViewBag.District = values.District;
            ViewBag.Address = values.Address;
            ViewBag.Type = values.Type;

            ViewBag.BathCount = values2.BathCount;
            ViewBag.BedCount = values2.BedRoomCount;
            ViewBag.Size = values2.ProductSize;

            int month = (DateTime.Now - values.AdvertisementDate).Days;

            ViewBag.dateDiff = month/30; //ilanın kaç gün önce verildiğini hesapladık

            return View();
        }
    }
}
