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

        public async Task<IActionResult> PropertyListWithSearch(string searchKeyValue, int propertyCategoryId, string city)
        {
            searchKeyValue = TempData["searchKeyValue"].ToString();
            propertyCategoryId = int.Parse(TempData["propertyCategoryId"].ToString());
            city = TempData["city"].ToString();

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7101/api/Products/ResultProductWithSearchList?searchKeyValue={searchKeyValue}&propertyCategoryId={propertyCategoryId}&city={city}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithSearchListDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet("property/{slug}/{id}")]
        public async Task<IActionResult> PropertySingle(string slug,int id)
        {
            ViewBag.i = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7101/api/Products/GetProductByProductId?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync(); //json veriyi stringe çevirdik
            var values = JsonConvert.DeserializeObject<GetProductByProductIdDto>(jsonData); //string veriyi nesneye çevirdik

            var responseMessage2 = await client.GetAsync("https://localhost:7101/api/ProductDetails/GetProductDetailByProductId?id=" + id);
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync(); //json veriyi stringe çevirdik
            var values2 = JsonConvert.DeserializeObject<GetProductDetailByIdDto>(jsonData2); //string veriyi nesneye çevirdik

            ViewBag.Tittle = values.Tittle.ToString();
            ViewBag.productId = values.ProductID;
            ViewBag.Price = values.Price;
            ViewBag.City = values.City;
            ViewBag.District = values.District;
            ViewBag.Address = values.Address;
            ViewBag.Type = values.Type;
            ViewBag.description = values.Description;
            ViewBag.Date = values.AdvertisementDate;
            ViewBag.SlugUrl = values.SlugUrl;

            ViewBag.BathCount = values2.BathCount;
            ViewBag.BedCount = values2.BedRoomCount;
            ViewBag.Size = values2.ProductSize;
            ViewBag.GarageSize = values2.GarageSize;
            ViewBag.RoomCount = values2.RoomCount;
            ViewBag.BuildYear = values2.BuildYear;
            ViewBag.Location = values2.Location;
            ViewBag.VideoUrl = values2.VideoUrl;
            

            int month = (DateTime.Now - values.AdvertisementDate).Days;

            ViewBag.dateDiff = month/30; //ilanın kaç gün önce verildiğini hesapladık

            string slugFromTitle = CreateSlug(values.Tittle);
            ViewBag.slugUrl = slugFromTitle;

            return View();
        }

        private string CreateSlug(string title)
        {
            title = title.ToLowerInvariant(); // Küçük harfe çevir
            title = title.Replace(" ", "-"); // Boşlukları tire ile değiştir
            title = System.Text.RegularExpressions.Regex.Replace(title, @"[^a-z0-9\s-]", ""); // Geçersiz karakterleri kaldır
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s+", " ").Trim(); // Birden fazla boşluğu tek boşluğa indir ve kenar boşluklarını kaldır
            title = System.Text.RegularExpressions.Regex.Replace(title, @"\s", "-"); // Boşlukları tire ile değiştir

            return title;
        }
    }
}
