using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lesson08.Lab.Models;
using X.PagedList;

namespace Lap8.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class BannersController : Controller
    {
        private readonly AppDbContext _context;

        public BannersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 10;
            var banners = await _context.Banners.OrderByDescending(b => b.Id).ToPagedListAsync(page, limit);
            if (!string.IsNullOrEmpty(name))
            {
                banners = await _context.Banners.Where(b => b.Name.Contains(name)).OrderByDescending(b => b.Id).ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(banners);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var banner = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null) return NotFound();
            return View(banner);
        }

        public IActionResult Create()
        {
            return View(new Banner { Status = 1, CreatedDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Prioty,Description")] Banner banner, IFormFile? ImageFile)
        {
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "banners");
                    Directory.CreateDirectory(folder);
                    var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(ImageFile.FileName)}";
                    using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                    await ImageFile.CopyToAsync(stream);
                    banner.Image = "/images/banners/" + fileName;
                }
                banner.CreatedDate = DateTime.Now;
                _context.Add(banner);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới banner thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null) return NotFound();
            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,Prioty,CreatedDate,Image,Description")] Banner banner, IFormFile? ImageFile)
        {
            if (id != banner.Id) return NotFound();
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "banners");
                        Directory.CreateDirectory(folder);
                        var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(ImageFile.FileName)}";
                        using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                        await ImageFile.CopyToAsync(stream);
                        banner.Image = "/images/banners/" + fileName;
                    }
                    _context.Update(banner);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật banner thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Banners.Any(e => e.Id == banner.Id)) return NotFound();
                    throw;
                }
            }
            return View(banner);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var banner = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null) return NotFound();
            return View(banner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner != null)
            {
                if (!string.IsNullOrEmpty(banner.Image))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", banner.Image.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(path)) try { System.IO.File.Delete(path); } catch { }
                }
                _context.Banners.Remove(banner);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa banner thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
