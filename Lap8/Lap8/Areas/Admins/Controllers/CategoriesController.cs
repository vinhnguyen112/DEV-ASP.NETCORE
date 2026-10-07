using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lesson08.Lab.Models;
using X.PagedList;

namespace Lap8.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admins/Categories
        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 5;
            //var categories = await _context.Categories
            //    .Include(c => c.Products)
            //    .OrderByDescending(c => c.Id)
            //    .ToListAsync();

            var categories = await _context.Categories.OrderBy(x => x.Id).ToPagedListAsync(page, limit);
            if (!string.IsNullOrEmpty(name))
            {
                categories = await _context.Categories.Where(x => x.Name.Contains(name)).OrderBy(x => x.Id).ToPagedListAsync(page, limit);

            }

            ViewBag.keyword = name;
            return View(categories);
        }

        // GET: Admins/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admins/Categories/Create
        public IActionResult Create()
        {
            var category = new Category
            {
                Status = 1,
                CreatedDate = DateTime.Now
            };
            return View(category);
        }

        // POST: Admins/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Description")] Category category, IFormFile? ImageFile)
        {
            // Kiá»ƒm tra trÃ¹ng tÃªn danh má»¥c
            if (!string.IsNullOrWhiteSpace(category.Name) && 
                await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "TÃªn danh má»¥c nÃ y Ä‘Ã£ tá»“n táº¡i trong há»‡ thá»‘ng.");
            }

            // Bá» kiá»ƒm tra ModelState cho Products vÃ  Image (náº¿u cÃ³ lá»—i ngáº§m)
            ModelState.Remove("Products");
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                // Xá»­ lÃ½ upload áº£nh
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "categories");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(ImageFile.FileName)}";
                    var filePath = Path.Combine(folderPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    category.Image = "/images/categories/" + fileName;
                }

                category.CreatedDate = DateTime.Now;

                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "ThÃªm má»›i danh má»¥c thÃ nh cÃ´ng!";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Admins/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admins/Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,CreatedDate,Image,Description")] Category category, IFormFile? ImageFile)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            // Kiá»ƒm tra trÃ¹ng tÃªn danh má»¥c vá»›i báº£n ghi khÃ¡c
            if (!string.IsNullOrWhiteSpace(category.Name) && 
                await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.Trim().ToLower() && c.Id != id))
            {
                ModelState.AddModelError("Name", "TÃªn danh má»¥c nÃ y Ä‘Ã£ tá»“n táº¡i trong há»‡ thá»‘ng.");
            }

            ModelState.Remove("Products");
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                try
                {
                    // Náº¿u cÃ³ táº£i áº£nh má»›i lÃªn thÃ¬ cáº­p nháº­t áº£nh
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "categories");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(ImageFile.FileName)}";
                        var filePath = Path.Combine(folderPath, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }
                        category.Image = "/images/categories/" + fileName;
                    }

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cáº­p nháº­t danh má»¥c thÃ nh cÃ´ng!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(category);
        }

        // GET: Admins/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admins/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                // XÃ³a file áº£nh váº­t lÃ½ náº¿u cÃ³
                if (!string.IsNullOrEmpty(category.Image))
                {
                    var relativePath = category.Image.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
                    if (System.IO.File.Exists(oldPath))
                    {
                        try { System.IO.File.Delete(oldPath); } catch { }
                    }
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "XÃ³a danh má»¥c thÃ nh cÃ´ng!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}

