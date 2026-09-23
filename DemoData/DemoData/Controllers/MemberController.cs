using DemoData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace DemoData.Controllers
{
    public class MemberController : Controller
    {
        public static List<Member> Members = new List<Member>()
        {
            new Member { MemberId = "1", UserName = "john_doe", FullName = "John Doe", Password = "password123", Email = "abc@gmail.com", Phone = "123-456-7890", Birthday = new DateTime(1990, 1, 1) },
            new Member { MemberId = "2", UserName = "jane_smith", FullName = "Jane Smith", Password = "password456", Email = "abd@gmail.com", Phone = "987-654-3210", Birthday = new DateTime(1992, 5, 15) },
        };

        public IActionResult Index()
        {
            return View(Members);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Created(Member member)
        {
            string Msg = "";
            bool validate = true;

            if (string.IsNullOrEmpty(member.UserName) || member.UserName.Length < 3 || member.UserName.Length > 20)
            {
                Msg += "<li>Tên đăng nhập phải từ 3 đến 20 ký tự</li>";
                validate = false;
            }

            string regxEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrEmpty(member.Email) || !Regex.IsMatch(member.Email, regxEmail))
            {
                Msg += "<li>Email không hợp lệ</li>";
                validate = false;
            }

            if (member.Birthday.AddYears(18) > DateTime.Now)
            {
                Msg += "<li>Bạn chưa đủ 18 tuổi để tham gia</li>";
                validate = false;
            }

            string regxPhone = @"^(\d{10,11}|\d{3}-\d{3}-\d{4}|\d{3}\s\d{3}\s\d{4})$";

            if (string.IsNullOrEmpty(member.Phone) || !Regex.IsMatch(member.Phone, regxPhone))
            {
                Msg += "<li>Số điện thoại không hợp lệ (Ví dụ: 0987654321 hoặc 123-456-7890)</li>";
                validate = false;
            }

            if (validate)
            {
                member.MemberId = Members.Count > 0 ? (int.Parse(Members.Max(m => m.MemberId)) + 1).ToString() : "1";
                Members.Add(member);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = Msg;
                return View("Create");
            }
        }
    }
}