using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
