using Labguide05.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq; // Cần thêm namespace này để dùng .Any()
using System.Text.RegularExpressions;

namespace Labguide05.Controllers // Đổi namespace thành Controllers cho chuẩn
{
    public class AccountController : Controller
    {
        // 1. Khai báo danh sách dữ liệu dùng chung cho toàn bộ Controller
        private static List<Account> accounts = new List<Account>
        {
            new Account { Id = 1, FullName = "Nguyễn Văn A", Phone = "0987654321" }
        };

        // GET: AccountController
        public ActionResult Index()
        {
            return View(accounts);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            Account model = new Account();
            return View(model);
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account model)
        {
            if (ModelState.IsValid)
            {
                model.Id = accounts.Count > 0 ? accounts.Max(a => a.Id) + 1 : 1;
                accounts.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
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

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
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

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPhone(string phone)
        {
            // Kiểm tra truyền giá trị rỗng
            if (string.IsNullOrEmpty(phone))
            {
                return Json("Số điện thoại không được để trống.");
            }

            // 1. Kiểm tra định dạng số điện thoại
            Regex isPhoneRegex = new Regex(@"^(0[3|5|7|8|9])+([0-9]{8})$");
            if (!isPhoneRegex.IsMatch(phone))
            {
                return Json($"Số điện thoại {phone} không hợp lệ (Ví dụ đúng: 0987654321).");
            }

            // 2. Kiểm tra trùng lặp trong danh sách accounts
            bool isExist = accounts.Any(a => a.Phone == phone);
            if (isExist)
            {
                return Json($"Số điện thoại {phone} đã được đăng ký.");
            }

            return Json(true);
        }
    }
}