using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lap06_BT.Models;
using Lap06_BT.Entities;
using Microsoft.AspNetCore.Hosting; // Bắt buộc thêm để dùng IWebHostEnvironment
using System.IO; // Bắt buộc thêm để xử lý file
using System;
using System.Threading.Tasks;
using System.Linq;

public class BannersController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment; // Khai báo biến môi trường

    // Hàm khởi tạo tiêm (Inject) DbContext và Biến môi trường hệ thống
    public BannersController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: BANNERS
    public async Task<IActionResult> Index()
    {
        return View(await _context.Banners.ToListAsync());
    }

    // GET: BANNERS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var banner = await _context.Banners
            .FirstOrDefaultAsync(m => m.Id == id);
        if (banner == null)
        {
            return NotFound();
        }

        return View(banner);
    }

    // GET: BANNERS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: BANNERS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,CreatedDate,Status,ImageFile")] Banner banner)
    {
        ModelState.Remove("Image"); // Bỏ qua xác thực chuỗi string Image gốc

        if (ModelState.IsValid)
        {
            // XỬ LÝ UPLOAD FILE ẢNH BANNER KHI TẠO MỚI
            if (banner.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string imagesPath = Path.Combine(wwwRootPath, "images");

                // Tự động tạo thư mục "images" nếu trong wwwroot chưa có
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                // Tạo tên file duy nhất bằng Guid tránh trùng lặp file trùng tên
                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(banner.ImageFile.FileName);
                string fileSavePath = Path.Combine(imagesPath, fileName);

                using (var fileStream = new FileStream(fileSavePath, FileMode.Create))
                {
                    await banner.ImageFile.CopyToAsync(fileStream);
                }

                banner.Image = fileName; // Lưu tên file vào trường Image dưới SQL Server
            }

            _context.Add(banner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(banner);
    }

    // GET: BANNERS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var banner = await _context.Banners.FindAsync(id);
        if (banner == null)
        {
            return NotFound();
        }
        return View(banner);
    }

    // POST: BANNERS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Image,Description,CreatedDate,Status,ImageFile")] Banner banner)
    {
        if (id != banner.Id)
        {
            return NotFound();
        }

        ModelState.Remove("Image");

        if (ModelState.IsValid)
        {
            try
            {
                // XỬ LÝ CẬP NHẬT ẢNH MỚI VÀ XÓA ẢNH CŨ CỦA BANNER
                if (banner.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    // 1. Xóa file ảnh cũ nếu đang tồn tại trong thư mục root
                    if (!string.IsNullOrEmpty(banner.Image))
                    {
                        string oldFilePath = Path.Combine(wwwRootPath, "images", banner.Image);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // 2. Upload tệp ảnh mới lên
                    string imagesPath = Path.Combine(wwwRootPath, "images");
                    string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(banner.ImageFile.FileName);
                    string fileSavePath = Path.Combine(imagesPath, fileName);

                    using (var fileStream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        await banner.ImageFile.CopyToAsync(fileStream);
                    }

                    banner.Image = fileName; // Gán lại tên file ảnh mới
                }

                _context.Update(banner);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BannerExists(banner.Id))
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
        return View(banner);
    }

    // GET: BANNERS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var banner = await _context.Banners
            .FirstOrDefaultAsync(m => m.Id == id);
        if (banner == null)
        {
            return NotFound();
        }

        return View(banner);
    }

    // POST: BANNERS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var banner = await _context.Banners.FindAsync(id);
        if (banner != null)
        {
            // XỬ LÝ XÓA FILE ẢNH KHỎI WWWROOT KHI BANNER BỊ XÓA KHỎI DATABASE
            if (!string.IsNullOrEmpty(banner.Image))
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string filePath = Path.Combine(wwwRootPath, "images", banner.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Banners.Remove(banner);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BannerExists(int id)
    {
        return _context.Banners.Any(e => e.Id == id);
    }
}
