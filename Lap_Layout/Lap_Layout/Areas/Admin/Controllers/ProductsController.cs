using Microsoft.AspNetCore.Mvc;

namespace Lap_Layout.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
