using System.Diagnostics;
using System.Threading.Tasks;
using Lap06_BT.Models;
using Lap06_BT.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lap06_BT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var banners = await _context.Banners
                .Where(b => b.Status == 1)
                .OrderByDescending(b => b.Id)
                .ToListAsync();
            return View(banners);
        }

        [Route("Home/Product")]
        [Route("Home/Products")]
        public async Task<IActionResult> Product()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
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

