using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.Controllers
{
    public class TestController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
