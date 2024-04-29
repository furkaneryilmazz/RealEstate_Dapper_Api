using Microsoft.AspNetCore.Mvc;

namespace RealEstate_Dapper_UI.ViewComponents.Layout
{
    public class _HeaderViewComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()//Invoke metodu ViewComponent sınıfından gelir ve ViewComponentResult döner.
        {
            return View();
        }
    }
}
