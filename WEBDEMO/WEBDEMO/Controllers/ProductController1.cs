using Microsoft.AspNetCore.Mvc;

namespace WEBDEMO.Controllers
{
    public class ProductController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
