using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.Controllers
{
    public class StatisticController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StatisticController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            #region ist1
            var client = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage = await client.GetAsync("https://localhost:7101/api/Statistics/ActiveCategoryCount");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.activeCategoryCount = jsonData;
            #endregion  

            #region ist2
            var client2 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage2 = await client2.GetAsync("https://localhost:7101/api/Statistics/ActiveEmployeeCount");
            var jsonData2 = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.ActiveEmployeeCount = jsonData2;
            #endregion  

            #region ist3
            var client3 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage3 = await client3.GetAsync("https://localhost:7101/api/Statistics/ApartmanCount");
            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            ViewBag.ApartmanCount = jsonData3;
            #endregion 

            #region ist4
            var client4 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage4 = await client4.GetAsync("https://localhost:7101/api/Statistics/AverageProductPriceByRent");
            var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
            ViewBag.AverageProductPriceByRent = jsonData4;
            #endregion 

            #region ist5
            var client5 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage5 = await client5.GetAsync("https://localhost:7101/api/Statistics/AverageProductPriceBySale");
            var jsonData5 = await responseMessage5.Content.ReadAsStringAsync();
            ViewBag.AverageProductPriceBySale = jsonData5;
            #endregion 

            #region ist6
            var client6 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage6 = await client6.GetAsync("https://localhost:7101/api/Statistics/AverageRoomCount");
            var jsonData6 = await responseMessage6.Content.ReadAsStringAsync();
            ViewBag.AverageRoomCount = jsonData6;
            #endregion 

            #region ist7
            var client7 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage7 = await client7.GetAsync("https://localhost:7101/api/Statistics/CategoryCount");
            var jsonData7 = await responseMessage7.Content.ReadAsStringAsync();
            ViewBag.CategoryCount = jsonData7;
            #endregion 

            #region ist8
            var client8 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage8 = await client8.GetAsync("https://localhost:7101/api/Statistics/CategoryNameByMaxProductCount");
            var jsonData8 = await responseMessage8.Content.ReadAsStringAsync();
            ViewBag.CategoryNameByMaxProductCount = jsonData8;
            #endregion 

            #region ist9
            var client9 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage9 = await client9.GetAsync("https://localhost:7101/api/Statistics/CityNameByMaxProductCount");
            var jsonData9 = await responseMessage9.Content.ReadAsStringAsync();
            ViewBag.CityNameByMaxProductCount = jsonData9;
            #endregion

            #region ist10
            var client10 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage10 = await client10.GetAsync("https://localhost:7101/api/Statistics/DifferentCityCount");
            var jsonData10 = await responseMessage10.Content.ReadAsStringAsync();
            ViewBag.DifferentCityCount = jsonData10;
            #endregion

            #region ist11
            var client11 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage11 = await client11.GetAsync("https://localhost:7101/api/Statistics/EmployeeNameByMaxProductCount");
            var jsonData11 = await responseMessage11.Content.ReadAsStringAsync();
            ViewBag.EmployeeNameByMaxProductCount = jsonData11;
            #endregion

            #region ist12
            var client12 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage12 = await client12.GetAsync("https://localhost:7101/api/Statistics/LastProductPrice");
            var jsonData12 = await responseMessage12.Content.ReadAsStringAsync();
            ViewBag.LastProductPrice = jsonData12;
            #endregion

            #region ist13
            var client13 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage13 = await client13.GetAsync("https://localhost:7101/api/Statistics/NewestBuildingYear");
            var jsonData13 = await responseMessage13.Content.ReadAsStringAsync();
            ViewBag.NewestBuildingYear = jsonData13;
            #endregion

            #region ist14
            var client14 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage14 = await client14.GetAsync("https://localhost:7101/api/Statistics/OldestBuildingYear");
            var jsonData14 = await responseMessage14.Content.ReadAsStringAsync();
            ViewBag.OldestBuildingYear = jsonData14;
            #endregion

            #region ist15
            var client15 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage15 = await client15.GetAsync("https://localhost:7101/api/Statistics/PassiveCategoryCount");
            var jsonData15 = await responseMessage15.Content.ReadAsStringAsync();
            ViewBag.PassiveCategoryCount = jsonData15;
            #endregion

            #region ist16
            var client16 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage16 = await client16.GetAsync("https://localhost:7101/api/Statistics/ProductCount");
            var jsonData16 = await responseMessage16.Content.ReadAsStringAsync();
            ViewBag.ProductCount = jsonData16;
            #endregion

            return View();
        }
    }
}
