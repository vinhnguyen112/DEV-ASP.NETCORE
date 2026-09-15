using Microsoft.AspNetCore.Mvc;
using WB3.Models;

namespace WB3.Controllers
{
    public class AccountController : Controller
    {
        private List<Account> GetAccounts()
        {
            return new List<Account>
            {
                new Account { id = 1, img = Url.Content("~/img/01.png"), name = "Avatar Dragon", price = 15.5f, description = "Tài khoản VIP rồng huyền thoại, nhiều vật phẩm hiếm." },
                new Account { id = 2, img = Url.Content("~/img/02.png"), name = "Avatar Knight", price = 20.0f, description = "Tài khoản hiệp sĩ bóng đêm, trang bị hoàng gia." },
                new Account { id = 3, img = Url.Content("~/img/03.png"), name = "Avatar Mage", price = 12.0f, description = "Tài khoản pháp sư ma thuật tối thượng." },
                new Account { id = 4, img = Url.Content("~/img/04.png"), name = "Avatar Archer", price = 18.0f, description = "Tài khoản xạ thủ tốc độ cao, độ chính xác tuyệt đối." },
                new Account { id = 5, img = Url.Content("~/img/05.png"), name = "Avatar Assassin", price = 25.0f, description = "Tài khoản sát thủ bóng đêm, tỉ lệ thắng cực cao." },
                new Account { id = 6, img = Url.Content("~/img/06.png"), name = "Avatar Warrior", price = 10.0f, description = "Tài khoản đấu sĩ càn lướt, phòng thủ tối đa." }
            };
        }

        public IActionResult Index()
        {
            List<Account> accounts = GetAccounts();
            ViewBag.Accounts = accounts;
            return View();
        }

        [Route("Profile/{id?}", Name = "profile")]
        public IActionResult Profile(int? id)
        {
            List<Account> accounts = GetAccounts();
            var account = accounts.FirstOrDefault(a => a.id == id) ?? accounts.FirstOrDefault();

            if (account == null)
            {
                return NotFound();
            }

            ViewBag.Account = account;
            return View();
        }
    }
}
