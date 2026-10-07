using Microsoft.AspNetCore.Mvc;

namespace Lap09.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class CategoryController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
