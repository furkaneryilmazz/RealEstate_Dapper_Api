using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_UI.Services;

namespace RealEstate_Dapper_UI.ViewComponents.EstateAgent
{
    public class _EstateAgentDashboardStatisticComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;

        public _EstateAgentDashboardStatisticComponentPartial(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = _loginService.GetUserID;
            #region ist1 - ToplamİlanSayısı
            var client1 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage1 = await client1.GetAsync("https://localhost:7101/api/EstateAgentDashboardStatistic/AllProductCount");
            var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
            ViewBag.ProductCount = jsonData1;
            #endregion

            #region ist2-EmlakcınınToplamİlanSayısı
            var client2 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage2 = await client2.GetAsync("https://localhost:7101/api/EstateAgentDashboardStatistic/ProductCountByEmployeeId?id="+id);
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            ViewBag.EmployeeByProductCount = jsonData2;
            #endregion


            #region ist3-EmlakcınınAktifİlanSayısı
            var client3 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage3 = await client3.GetAsync("https://localhost:7101/api/EstateAgentDashboardStatistic/ProductCountByStatusTrue?id=" + id);
            var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
            ViewBag.ProductCountByEmployeeStatusTrue = jsonData3;
            #endregion

            #region ist4-OrtalamaKiralıkFiyat
            var client4 = _httpClientFactory.CreateClient(); // Create a new HttpClient instance
            var responseMessage4 = await client4.GetAsync("https://localhost:7101/api/EstateAgentDashboardStatistic/ProductCountByStatusFalse?id=" + id);
            var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
            ViewBag.ProductCountByEmployeeStatusFalse = jsonData4;
            #endregion 

            return View();
        }
    }
}
