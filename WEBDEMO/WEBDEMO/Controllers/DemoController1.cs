using Microsoft.AspNetCore.Mvc;

namespace WEBDEMO.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
