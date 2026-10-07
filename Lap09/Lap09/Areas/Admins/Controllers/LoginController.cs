using Lap09.Areas.Admins.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lap09.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class LoginController : Controller
    {
        [HttpGet] // Get, hiển thị form để nhập dữ liệu
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost] // POST -> khi submit form
        public IActionResult Index(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // trả về trạng thái lỗi
            }


            HttpContext.Session.SetString("AdminLogin", model.Email); // Lưu thông tin đăng nhập vào session
            // sẽ xử lý logic phần đăng nhập tại đây
            return RedirectToAction("Index", "Dashboard" ); // chuyển hướng đến trang Dashboard của Admins


        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminLogin"); // Xóa thông tin đăng nhập khỏi session
            return RedirectToAction("Index"); // Chuyển hướng về trang đăng nhập
        }

    }
}
