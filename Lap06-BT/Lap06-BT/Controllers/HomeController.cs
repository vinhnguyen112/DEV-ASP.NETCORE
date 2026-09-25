using System.Diagnostics;
using System.Threading.Tasks;
using Lap06_BT.Models;
using Lap06_BT.Entities; // Bắt buộc thêm để hệ thống nhận diện AppDbContext
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Thêm để sử dụng hàm ToListAsync

namespace Lap06_BT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context; // 1. KHAI BÁO BIẾN DATABASE CONTEXT

        // 2. CẬP NHẬT HÀM KHỞI TẠO: Tiêm thêm AppDbContext vào hệ thống
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context; // Gán giá trị kết nối dữ liệu
        }

        // 3. THÊM ACTION PRODUCT THEO YÊU CẦU BÀI 2
        [Route("Home/Product")]
        [Route("Home/Products")]
        public async Task<IActionResult> Product()
        {
            // Lấy danh sách sản phẩm từ SQL Server truyền sang giao diện View
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // Mở file Controllers/HomeController.cs và sửa lại hàm Index như sau:
        public async Task<IActionResult> Index()
        {
            // Lấy danh sách banner đang kích hoạt (Status = 1) đổ ra trang chủ
            var banners = await _context.Banners.Where(b => b.Status == 1).ToListAsync();
            return View(banners); // Truyền danh sách banner sang View Index
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
