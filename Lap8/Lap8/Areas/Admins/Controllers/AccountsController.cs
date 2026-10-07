using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lesson08.Lab.Models;
using X.PagedList;

namespace Lap8.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class AccountsController : Controller
    {
        private readonly AppDbContext _context;

        public AccountsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 10;
            var accounts = await _context.Accounts.OrderByDescending(a => a.Id).ToPagedListAsync(page, limit);
            if (!string.IsNullOrEmpty(name))
            {
                accounts = await _context.Accounts.Where(a => a.Name.Contains(name)).OrderByDescending(a => a.Id).ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(accounts);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);
            if (account == null) return NotFound();
            return View(account);
        }

        public IActionResult Create()
        {
            return View(new Account());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password")] Account account, IFormFile? AvatarFile)
        {
            ModelState.Remove("Avatar");

            if (ModelState.IsValid)
            {
                if (AvatarFile != null && AvatarFile.Length > 0)
                {
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "accounts");
                    Directory.CreateDirectory(folder);
                    var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(AvatarFile.FileName)}";
                    using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                    await AvatarFile.CopyToAsync(stream);
                    account.Avatar = "/images/accounts/" + fileName;
                }

                _context.Add(account);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới tài khoản thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return NotFound();
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Avatar")] Account account, IFormFile? AvatarFile)
        {
            if (id != account.Id) return NotFound();

            ModelState.Remove("Avatar");

            if (ModelState.IsValid)
            {
                try
                {
                    if (AvatarFile != null && AvatarFile.Length > 0)
                    {
                        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "accounts");
                        Directory.CreateDirectory(folder);
                        var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(AvatarFile.FileName)}";
                        using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                        await AvatarFile.CopyToAsync(stream);
                        account.Avatar = "/images/accounts/" + fileName;
                    }

                    _context.Update(account);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật tài khoản thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Accounts.AnyAsync(e => e.Id == account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(account);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);
            if (account == null) return NotFound();
            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa tài khoản thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
