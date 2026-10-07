using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lesson08.Lab.Models;
using X.PagedList;


namespace Lap8.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 10;
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .ToPagedListAsync(page, limit);

            if (!string.IsNullOrEmpty(name))
            {
                products = await _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.Name.Contains(name))
                    .OrderByDescending(p => p.Id)
                    .ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name");
            return View(new Product { Status = 1, CreatedDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,SalePrice,Status,Description,CategoryId")] Product product, IFormFile? ImageFile)
        {
            ModelState.Remove("Image");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                    Directory.CreateDirectory(folder);
                    var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(ImageFile.FileName)}";
                    using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                    await ImageFile.CopyToAsync(stream);
                    product.Image = "/images/products/" + fileName;
                }
                product.CreatedDate = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới sản phẩm thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,SalePrice,Status,Description,CategoryId,CreatedDate,Image")] Product product, IFormFile? ImageFile)
        {
            if (id != product.Id) return NotFound();
            ModelState.Remove("Image");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                        Directory.CreateDirectory(folder);
                        var fileName = $"{Guid.NewGuid().ToString()[..8]}_{Path.GetFileName(ImageFile.FileName)}";
                        using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
                        await ImageFile.CopyToAsync(stream);
                        product.Image = "/images/products/" + fileName;
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật sản phẩm thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(e => e.Id == product.Id)) return NotFound();
                    throw;
                }
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.Name), "Id", "Name", product.CategoryId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.Image.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(path)) try { System.IO.File.Delete(path); } catch { }
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa sản phẩm thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
