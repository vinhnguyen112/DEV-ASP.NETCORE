using System.Diagnostics;
using Lap_Layout.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lap_Layout.Controllers
{
    public class ProductsController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Search()
        {
            return View(); 
        }
        public IActionResult Hots()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
