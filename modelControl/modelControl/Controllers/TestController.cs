using Microsoft.AspNetCore.Mvc;
using modelControl.Models;

namespace modelControl.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.MessageVB = "Test DetailsWB";
            ViewData["MessageVD"] = "Test DetailsWD";
            TempData["MessageTD"] = "Test DetailsTD";
            return View();
        }

        public IActionResult GetTest()
        {
            List<Test> tests = new List<Test>
            {
                new Test { Id = 1, Name = "Test 1", YearRelease = 2023, Price = 99.99f, Description = "Description for Test 1" },
                new Test { Id = 2, Name = "Test 2", YearRelease = 2022, Price = 149.99f, Description = "Description for Test 2" },
                new Test { Id = 3, Name = "Test 3", YearRelease = 2021, Price = 199.99f, Description = "Description for Test 3" }
            };
            ViewBag.Test = tests;
            return View("GetTest");
        }
    }
}
