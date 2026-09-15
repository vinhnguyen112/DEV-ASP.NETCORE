using Microsoft.AspNetCore.Mvc;
using WB1.Models;

namespace WB1.Controllers
{
    public class AccController : Controller
    {
        // Danh sách dữ liệu dùng chung
        private readonly List<Acc> accs = new()
        {
            new Acc { img = "~/img/01.png", Name = "Acc 1", Price = 10.99f },
            new Acc { img = "~/img/02.png", Name = "Acc 2", Price = 15.49f },
            new Acc { img = "~/img/03.png", Name = "Acc 3", Price = 7.99f },
            new Acc { img = "~/img/04.png", Name = "Acc 4", Price = 12.75f },
            new Acc { img = "~/img/05.png", Name = "Acc 5", Price = 9.50f }
        };

        public IActionResult Index()
        {
            return View(accs);
        }

        public IActionResult Details(string name)
        {
            // Tìm theo tên và truyền thẳng qua ViewBag
            ViewBag.Acc = accs.FirstOrDefault(x => x.Name == name);
            return View();
        }
    }
}