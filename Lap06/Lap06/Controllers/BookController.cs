using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lap06.Models.DBModel;

namespace Lap06.Controllers
{
    public class BookController : Controller
    {
        private readonly BookStoreContext _context;

        public BookController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            // Sử dụng .Include để nạp đầy đủ dữ liệu từ bảng liên kết Category và Publisher
            var fullDanhSachSach = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Publisher)
                .ToListAsync();

            return View(fullDanhSachSach);
        }


        // GET: Book/Details/P63952
        // Đổi "bookid" thành "id" để khớp với Route cấu hình hệ thống
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books
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
        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book)
        {
            // 💡 XÓA BỎ kiểm tra bắt buộc đối với các thực thể liên kết phức tạp
            ModelState.Remove("Category");
            ModelState.Remove("Publisher");
            ModelState.Remove("OrderDetails");

            // Kiểm tra xem Mã sách nhập vào đã bị trùng trong Database hay chưa
            if (_context.Books.Any(b => b.BookId == book.BookId))
            {
                ModelState.AddModelError("BookId", "Mã sách này đã tồn tại trong hệ thống! Vui lòng nhập mã khác.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Bắt lỗi nếu nhập sai mã ID của Thể loại hoặc Nhà xuất bản không tồn tại trong SQL
                    ModelState.AddModelError("", "Không thể thêm sách. Vui lòng kiểm tra lại Mã loại hoặc Mã NXB xem có tồn tại trong hệ thống không!");
                }
            }
            return View(book);
        }


        // ==========================================
        // 4. CHỈNH SỬA SÁCH (EDIT)
        // ==========================================
        // GET: Book/Edit/P63952
        // Đổi "bookid" thành "id" để xử lý dứt điểm lỗi đơ/lỗi hỏng khi nhấn nút Edit
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
            return View(book);
        }

        // POST: Book/Edit/P63952
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Đổi tham số nhận dữ liệu sang "id" để đồng bộ
        public async Task<IActionResult> Edit(string? id, [Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

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
            return View(book);
        }

        // ==========================================
        // 5. XÓA SÁCH (DELETE)
        // ==========================================
        // GET: Book/Delete/P63952
        // Đổi "bookid" thành "id"
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/P63952
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
