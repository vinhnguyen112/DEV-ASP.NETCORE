using Lap04.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lap04.Controllers
{
    public class NTVMembersController : Controller
    {
        public static List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện tử" },
            new Category { Id = 2, Name = "Thời trang" },
            new Category { Id = 3, Name = "Thực phẩm" },
            new Category { Id = 4, Name = "Đồ gia dụng" },
            new Category { Id = 5, Name = "Sách & Văn phòng phẩm" },
        };
        // GET: NTVMembersController
        public ActionResult Index()
        {
            return View(_categories);
        }

        // GET: NTVMembersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NTVMembersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NTVMembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NTVMembersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NTVMembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NTVMembersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NTVMembersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
