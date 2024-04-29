using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstate_Dapper_UI.Dtos.ServiceDtos;
using RealEstate_Dapper_UI.Dtos.WhoWeAreDtos;

namespace RealEstate_Dapper_UI.ViewComponents.HomePage
{
    public class _DefaultWhoWeAreComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory; //burada ne yaptık? HttpClientFactory sınıfından bir nesne oluşturduk. Bu nesne ile HttpClient sınıfını kullanarak API'ye istek atacağız.

        public _DefaultWhoWeAreComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var client2 = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://localhost:7101/api/WhoWeAreDetail");
            var responseMessage2 = await client2.GetAsync("https://localhost:7101/api/Services");

            if (responseMessage.IsSuccessStatusCode && responseMessage2.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();

                var value = JsonConvert.DeserializeObject<List<ResultWhoWeAreDetailDto>>(jsonData);
                var value2 = JsonConvert.DeserializeObject<List<ResultServiceDto>>(jsonData2);

                ViewBag.tittle= value.Select(x =>x.Tittle).FirstOrDefault();
                ViewBag.subTitle= value.Select(x =>x.Subtitle).FirstOrDefault();
                ViewBag.description1= value.Select(x =>x.Description1).FirstOrDefault();
                ViewBag.description2= value.Select(x =>x.Description2).FirstOrDefault();
                return View(value2);
            }
            return View();
        }
    }
}
