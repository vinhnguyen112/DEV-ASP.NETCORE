using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Labguide05.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labguide05.Controllers
{
    public class ProductController : Controller
    {
        // Danh sách mẫu
        private static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại" },
            new Category { Id = 2, Name = "Laptop" }
        };

        private static List<Product> products = new List<Product>();

        // Hàm hỗ trợ nạp danh mục vào ViewBag cho Dropdown
        private void LoadCategories()
        {
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
        }

        // GET: Danh sách sản phẩm
        public IActionResult Index()
        {
            return View(products);
        }

        // GET: Xem chi tiết
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            ViewBag.CategoryName = categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name;
            return View(product);
        }

        // GET: Form Thêm mới
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        // POST: Form Thêm mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product model, IFormFile ImageFile)
        {
            // 1. Kiểm tra bắt buộc chọn file ảnh
            if (ImageFile == null || ImageFile.Length == 0)
            {
                ModelState.AddModelError("Image", "Vui lòng chọn và upload ảnh sản phẩm");
            }
            else
            {
                // QUAN TRỌNG: Loại bỏ lỗi validation mặc định của trường Image
                // vì trường này sẽ được gán chuỗi đường dẫn sau khi upload thành công
                ModelState.Remove("Image");
            }

            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh vào folder wwwroot/products
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = System.Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }

                // Gán đường dẫn file ảnh tương đối
                model.Image = "/products/" + uniqueFileName;
                model.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
                products.Add(model);

                return RedirectToAction(nameof(Index));
            }

            LoadCategories();
            return View(model);
        }

        // GET: Chỉnh sửa
        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            LoadCategories();
            return View(product);
        }

        // POST: Chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product model, IFormFile? ImageFile)
        {
            if (id != model.Id) return BadRequest();

            // Loại bỏ kiểm tra Image khi chỉnh sửa (nếu không chọn ảnh mới thì giữ nguyên ảnh cũ)
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                var existing = products.FirstOrDefault(p => p.Id == id);
                if (existing != null)
                {
                    // Nếu người dùng chọn ảnh mới thì upload và thay thế
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "products");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        string uniqueFileName = System.Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(fileStream);
                        }
                        existing.Image = "/products/" + uniqueFileName;
                    }

                    existing.Name = model.Name;
                    existing.Price = model.Price;
                    existing.SalePrice = model.SalePrice;
                    existing.Description = model.Description;
                    existing.CategoryId = model.CategoryId;
                }
                return RedirectToAction(nameof(Index));
            }

            LoadCategories();
            return View(model);
        }

        // GET: Xóa
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}