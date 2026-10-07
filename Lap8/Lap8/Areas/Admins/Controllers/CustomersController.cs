using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lesson08.Lab.Models;
using X.PagedList;


namespace Lap8.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 10;
            var customers = await _context.Customers.OrderByDescending(c => c.Id).ToPagedListAsync(page, limit);
            if (!string.IsNullOrEmpty(name))
            {
                customers = await _context.Customers.Where(c => c.FullName.Contains(name)).OrderByDescending(c => c.Id).ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(customers);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        public IActionResult Create()
        {
            return View(new Customer { Birthday = DateTime.Today.AddYears(-20) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Phone,Address,Birthday,Gender,Password,Facebook")] Customer customer, IFormFile? AvatarFile)
        {
            ModelState.Remove("Avatar");
            ModelState.Remove("Gender");
            ModelState.Remove("Facebook");

            if (ModelState.IsValid)
            {
                if (AvatarFile != null && AvatarFile.Length > 0)
                {
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "customers");
                    Directory.CreateDirectory(folder);
                    var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(AvatarFile.FileName)}";
                    using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                    await AvatarFile.CopyToAsync(stream);
                    customer.Avatar = "/images/customers/" + fileName;
                }

                _context.Add(customer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới khách hàng thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Phone,Address,Avatar,Birthday,Gender,Password,Facebook")] Customer customer, IFormFile? AvatarFile)
        {
            if (id != customer.Id) return NotFound();

            ModelState.Remove("Avatar");
            ModelState.Remove("Gender");
            ModelState.Remove("Facebook");

            if (ModelState.IsValid)
            {
                try
                {
                    if (AvatarFile != null && AvatarFile.Length > 0)
                    {
                        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "customers");
                        Directory.CreateDirectory(folder);
                        var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(AvatarFile.FileName)}";
                        using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                        await AvatarFile.CopyToAsync(stream);
                        customer.Avatar = "/images/customers/" + fileName;
                    }

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật thông tin khách hàng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Customers.AnyAsync(e => e.Id == customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa khách hàng thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
