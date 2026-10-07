using Microsoft.AspNetCore.Mvc;

namespace Lap09.Areas.Admins.Controllers
{
    [Area("Admins")] // Thêm dòng này để khớp với tên thư mục Area
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}