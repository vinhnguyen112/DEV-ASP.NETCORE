using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WB5.Models;

namespace WB5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Áo dài" },
                new Category { Id = 2, Name = "Áo đông" },
                new Category { Id = 3, Name = "Túi xách" },
                new Category { Id = 4, Name = "Đồng hồ" },
                new Category { Id = 5, Name = "Ví da" }
            };
            ViewBag.Categories = categories;

            var latestProducts = new List<Product>
{
                new Product { Id = 1, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", ImageUrl = "/img/04.png" },
                new Product { Id = 2, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", ImageUrl = "/img/07.png" },
                new Product { Id = 3, Name = "Nồi cơm điện cao tần Nagakawa NAG0102", ImageUrl = "/img/10.png" }
};

            return View(latestProducts);
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
