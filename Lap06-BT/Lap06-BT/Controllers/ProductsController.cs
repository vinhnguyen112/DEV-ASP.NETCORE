using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lap06_BT.Models;
using Lap06_BT.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Hosting; // Bắt buộc thêm để dùng IWebHostEnvironment
using System.IO; // Bắt buộc thêm để xử lý file
using System;

public class ProductsController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment; // Khai báo biến môi trường

    // Cập nhật hàm khởi tạo để tiêm (Inject) dịch vụ hệ thống vào
    public ProductsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: PRODUCTS
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products.Include(p => p.Category).ToListAsync();
        return View(products);
    }

    // GET: PRODUCTS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: PRODUCTS/Create
    public IActionResult Create()
    {
        ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    // POST: PRODUCTS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Price,SalePrice,Status,Descriptions,CategoryId,CreatedDate,ImageFile")] Product product)
    {
        ModelState.Remove("Category");
        ModelState.Remove("Image");

        if (ModelState.IsValid)
        {
            // XỬ LÝ UPLOAD FILE ẢNH KHI TẠO MỚI
            if (product.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagesPath = Path.Combine(wwwRootPath, "images");

                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(product.ImageFile.FileName);
                string fileSavePath = Path.Combine(imagesPath, fileName);

                using (var fileStream = new FileStream(fileSavePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(fileStream);
                }

                product.Image = fileName; // Lưu tên file vào database
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    // GET: PRODUCTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    // POST: PRODUCTS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Image,Price,SalePrice,Status,Descriptions,CategoryId,CreatedDate,ImageFile")] Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        ModelState.Remove("Category");
        ModelState.Remove("Image");

        if (ModelState.IsValid)
        {
            try
            {
                // XỬ LÝ CẬP NHẬT ẢNH MỚI VÀ XÓA ẢNH CŨ
                if (product.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    // 1. Xóa file ảnh cũ nếu có để tránh rác bộ nhớ root
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        string oldFilePath = Path.Combine(wwwRootPath, "images", product.Image);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // 2. Upload file ảnh mới lên
                    string imagesPath = Path.Combine(wwwRootPath, "images");
                    string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(product.ImageFile.FileName);
                    string fileSavePath = Path.Combine(imagesPath, fileName);

                    using (var fileStream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                    }

                    product.Image = fileName; // Gán tên file ảnh mới
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        return View(product);
    }

    // GET: PRODUCTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: PRODUCTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            // XỬ LÝ XÓA FILE ẢNH KHỎI WWWROOT KHI XÓA SẢN PHẨM
            if (!string.IsNullOrEmpty(product.Image))
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string filePath = Path.Combine(wwwRootPath, "images", product.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Products.Remove(product);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int? id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}
