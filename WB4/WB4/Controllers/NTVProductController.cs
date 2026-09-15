using Microsoft.AspNetCore.Mvc;

namespace WB4.Controllers
{
    public class NTVProductController : Controller
    {
        public IActionResult Index(int ?pid)
        {
            ViewBag.pid = pid;
            return View();
        }
    }
}
