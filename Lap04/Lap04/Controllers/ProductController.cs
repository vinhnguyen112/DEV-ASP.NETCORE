using Lap04.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lap04.Controllers
{
    public class ProductController : Controller
    {
        // Lấy danh sách Category cho SelectList (ComboBox)
        private void LoadCategorySelectList(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(DataLocal.GetCategories(), "Id", "Name", selectedId);
        }

        // GET: Product
        public ActionResult Index()
        {
            var products = DataLocal.GetProducts();
            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = DataLocal.GetProductById(id);
            if (product == null)
                return NotFound();

            // Truyền tên Category qua ViewBag để hiển thị
            var category = DataLocal.GetCategoryById(product.CategoryId);
            ViewBag.CategoryName = category?.Name ?? "Không xác định";

            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            LoadCategorySelectList();
            var product = new Product
            {
                CreatedDate = DateTime.Now,
                Status = 1
            };
            return View(product);
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model, IFormFile? ImageFile)
        {
            try
            {
                // Xử lý upload ảnh
                var file = ImageFile ?? (HttpContext.Request.Form.Files.Count > 0 ? HttpContext.Request.Form.Files[0] : null);
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    var path = Path.Combine(folder, fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    model.Image = "/images/products/" + fileName;
                }
                else if (string.IsNullOrEmpty(model.Image))
                {
                    model.Image = "/images/products/default.png";
                }

                // Tự sinh Id
                model.Id = DataLocal._products.Any() ? DataLocal._products.Max(p => p.Id) + 1 : 1;

                DataLocal._products.Add(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                LoadCategorySelectList(model.CategoryId);
                return View(model);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = DataLocal.GetProductById(id);
            if (product == null)
                return NotFound();

            LoadCategorySelectList(product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product model)
        {
            try
            {
                // Xử lý upload ảnh mới (nếu có)
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var fileName = Path.GetFileName(file.FileName);
                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    var path = Path.Combine(folder, fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    model.Image = "/images/products/" + fileName;
                }

                // Cập nhật sản phẩm trong danh sách
                for (int i = 0; i < DataLocal._products.Count; i++)
                {
                    if (DataLocal._products[i].Id == id)
                    {
                        model.Id = id;
                        DataLocal._products[i] = model;
                        break;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                LoadCategorySelectList(model.CategoryId);
                return View(model);
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var product = DataLocal.GetProductById(id);
            if (product == null)
                return NotFound();

            var category = DataLocal.GetCategoryById(product.CategoryId);
            ViewBag.CategoryName = category?.Name ?? "Không xác định";

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product model)
        {
            try
            {
                for (int i = 0; i < DataLocal._products.Count; i++)
                {
                    if (DataLocal._products[i].Id == id)
                    {
                        DataLocal._products.RemoveAt(i);
                        break;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
