using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using WB6.Models.DataModels;

namespace WB6.Controllers
{
    public class NTVMemberController : Controller
    {
        private static readonly List<NTVMember> _members = new List<NTVMember>()
            {
                new NTVMember
    {
        NTVMemberId = Guid.NewGuid().ToString(),
        NTVUserName = "nguyenvana",
        NTVEmail = "vana@gmail.com",
        NTVFullName = "Nguyen Van A",
        NTVPassword = "Password@123"
    },
                new NTVMember
    {
        NTVMemberId = Guid.NewGuid().ToString(),
        NTVUserName = "tranthib",
        NTVEmail = "thib@gmail.com",
        NTVFullName = "Tran Thi B",
        NTVPassword = "Password@456"
    },
                new NTVMember
    {
        NTVMemberId = Guid.NewGuid().ToString(),
        NTVUserName = "levanc",
        NTVEmail = "vanc@gmail.com",
        NTVFullName = "Le Van C",
        NTVPassword = "Password@789"
    },
                new NTVMember
    {
        NTVMemberId = Guid.NewGuid().ToString(),
        NTVUserName = "phamthid",
        NTVEmail = "thid@gmail.com",
        NTVFullName = "Pham Thi D",
        NTVPassword = "Password@321"
    },
                new NTVMember
    {
        NTVMemberId = Guid.NewGuid().ToString(),
        NTVUserName = "hoangvane",
        NTVEmail = "vane@gmail.com",
        NTVFullName = "Hoang Van E",
        NTVPassword = "Password@654"
    }

            };
        public IActionResult Index()
        {
            return View(_members);
        }
        public IActionResult GetMember()
        {
            // Here you would typically fetch the NTVMember details from a database or service using the provided id.
            // For demonstration purposes, we'll create a dummy NTVMember object.
            var member = new NTVMember();
            member.NTVMemberId = Guid.NewGuid().ToString();
            member.NTVUserName = "SampleUserName";
            member.NTVEmail = "sample@example.com";
            member.NTVFullName = "Sample Full Name";
            member.NTVPassword = "SamplePassword";

            ViewBag.NTVmember = member;
            return View(member);
        }

        public ActionResult Create()
        {
            var member = new NTVMember();
            return View(member);
        }

        [HttpPost]
        public ActionResult Create(NTVMember member)
        {
           member.NTVMemberId = Guid.NewGuid().ToString();
            _members.Add(member);
            return RedirectToAction("Index");
        }


    }

}
