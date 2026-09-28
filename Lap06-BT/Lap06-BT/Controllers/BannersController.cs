using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lap06_BT.Entities;
using Lap06_BT.Models;

namespace Lap06_BT.Controllers
{
    public class BannersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BannersController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Banners
        public async Task<IActionResult> Index()
        {
            return View(await _context.Banners.ToListAsync());
        }

        // GET: Banners/Details/5
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

        // GET: Banners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CreatedDate,Status,ImageFile")] Banner banner)
        {
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                if (banner.ImageFile != null)
                {
                    string imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    if (!Directory.Exists(imagesPath))
                    {
                        Directory.CreateDirectory(imagesPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(banner.ImageFile.FileName);
                    string fileSavePath = Path.Combine(imagesPath, fileName);

                    using (var fileStream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        await banner.ImageFile.CopyToAsync(fileStream);
                    }

                    banner.Image = fileName;
                }

                _context.Add(banner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        // GET: Banners/Edit/5
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

        // POST: Banners/Edit/5
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
                    if (banner.ImageFile != null)
                    {
                        string imagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        if (!string.IsNullOrEmpty(banner.Image))
                        {
                            string oldFilePath = Path.Combine(imagesPath, banner.Image);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(banner.ImageFile.FileName);
                        string fileSavePath = Path.Combine(imagesPath, fileName);

                        using (var fileStream = new FileStream(fileSavePath, FileMode.Create))
                        {
                            await banner.ImageFile.CopyToAsync(fileStream);
                        }

                        banner.Image = fileName;
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

        // GET: Banners/Delete/5
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

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner != null)
            {
                if (!string.IsNullOrEmpty(banner.Image))
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", banner.Image);
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
}

