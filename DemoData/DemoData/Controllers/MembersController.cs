using DemoData.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoData.Controllers
{
    public class MembersController : Controller
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
        public IActionResult Create(Register model)
        {
            if (ModelState.IsValid)
            {
                Member member = new Member
                {
                    MemberId = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    FullName = model.FullName,
                    Password = model.Password,
                    Email = model.Email,
                    Phone = model.Phone,
                    Birthday = model.Birthday
                };
                Members.Add(member);
                return RedirectToAction("Index");
            }
            else 
            {
                return View(model);
            }
        }
    }
}
