using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lesson08.Lab.Models;
using X.PagedList;

namespace Lap8.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class BlogsController : Controller
    {
        private readonly AppDbContext _context;

        public BlogsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 10;
            var blogs = await _context.Blogs.OrderByDescending(b => b.Id).ToPagedListAsync(page, limit);
            if (!string.IsNullOrEmpty(name))
            {
                blogs = await _context.Blogs.Where(b => b.Name.Contains(name)).OrderByDescending(b => b.Id).ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(blogs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        public IActionResult Create()
        {
            return View(new Blog { Status = 1, CreatedDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Description")] Blog blog, IFormFile? ImageFile)
        {
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "blogs");
                    Directory.CreateDirectory(folder);
                    var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(ImageFile.FileName)}";
                    using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                    await ImageFile.CopyToAsync(stream);
                    blog.Image = "/images/blogs/" + fileName;
                }
                blog.CreatedDate = DateTime.Now;
                _context.Add(blog);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới bài viết thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,CreatedDate,Image,Description")] Blog blog, IFormFile? ImageFile)
        {
            if (id != blog.Id) return NotFound();
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "blogs");
                        Directory.CreateDirectory(folder);
                        var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(ImageFile.FileName)}";
                        using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                        await ImageFile.CopyToAsync(stream);
                        blog.Image = "/images/blogs/" + fileName;
                    }
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật bài viết thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Blogs.Any(e => e.Id == blog.Id)) return NotFound();
                    throw;
                }
            }
            return View(blog);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                if (!string.IsNullOrEmpty(blog.Image))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", blog.Image.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(path)) try { System.IO.File.Delete(path); } catch { }
                }
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa bài viết thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
