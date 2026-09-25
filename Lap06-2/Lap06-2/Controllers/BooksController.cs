using Lap06.Models.DBModel;
using Lap06_2.Models.BusinessModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Thêm thư viện này để hỗ trợ Dropdown List
using Microsoft.EntityFrameworkCore;

namespace Lap06_2.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookManagementContext _context;

        public BooksController(BookManagementContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. XEM DANH SÁCH ĐẦY ĐỦ (INDEX)
        // ==========================================
        // GET: Books
        public async Task<IActionResult> Index()
        {
            // Sử dụng .Include để nạp đầy đủ dữ liệu chữ từ bảng liên kết Category và Publisher
            var fullDanhSachSach = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .ToListAsync();

            return View(fullDanhSachSach);
        }

        // ==========================================
        // 2. XEM CHI TIẾT (DETAILS)
        // ==========================================
        // GET: Books/Details/B01
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // ==========================================
        // 3. THÊM MỚI SÁCH (CREATE)
        // ==========================================
        // GET: Books/Create
        public IActionResult Create()
        {
            // Nạp danh sách Thể loại và Nhà xuất bản truyền sang làm Dropdown List chọn bằng chuột
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book)
        {
            // Loại bỏ bắt buộc kiểm tra đối với các thực thể liên kết dữ liệu phức tạp
            ModelState.Remove("Category");
            ModelState.Remove("Publisher");
            ModelState.Remove("OrderDetails");

            // Kiểm tra trùng mã sách
            if (_context.Books.Any(b => b.BookId == book.BookId))
            {
                ModelState.AddModelError("BookId", "Mã sách này đã tồn tại trong hệ thống!");
            }

            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // ==========================================
        // 4. CHỈNH SỬA SÁCH (EDIT)
        // ==========================================
        // GET: Books/Edit/B01
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Nạp danh sách Thể loại và Nhà xuất bản chọn bằng chuột cho form Sửa
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // POST: Books/Edit/B01
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string? id, [Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            // Loại bỏ bắt buộc đối với các thực thể phức tạp gây đơ nút Save
            ModelState.Remove("Category");
            ModelState.Remove("Publisher");
            ModelState.Remove("OrderDetails");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName", book.PublisherId);
            return View(book);
        }

        // ==========================================
        // 5. XÓA SÁCH (DELETE)
        // ==========================================
        // GET: Books/Delete/B01
        // Đã sửa "bookid" thành tham số định tuyến chuẩn "id" để khắc phục lỗi 404
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/B01
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // Đã sửa thành "id" để nút bấm Xác nhận xóa hoạt động chính xác
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(string id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
