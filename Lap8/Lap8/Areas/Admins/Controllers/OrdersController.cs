using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lesson08.Lab.Models;
using X.PagedList;


namespace Lap8.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string name, int page = 1)
        {
            int limit = 10;
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .OrderByDescending(o => o.Id)
                .ToPagedListAsync(page, limit);

            if (!string.IsNullOrEmpty(name))
            {
                orders = await _context.Orders
                    .Include(o => o.Customer)
                    .Where(o => o.Name.Contains(name))
                    .OrderByDescending(o => o.Id)
                    .ToPagedListAsync(page, limit);
            }
            ViewBag.keyword = name;
            return View(orders);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null) return NotFound();

            ViewBag.OrderDetails = await _context.OrderDetails
                .Include(od => od.Product)
                .Where(od => od.OrderId == id)
                .ToListAsync();

            return View(order);
        }

        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers.OrderBy(c => c.FullName), "Id", "FullName");
            return View(new Orders { CreatedDate = DateTime.Now, Status = 0 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,Name,Email,Address,Status")] Orders orders)
        {
            ModelState.Remove("Customer");

            if (ModelState.IsValid)
            {
                orders.CreatedDate = DateTime.Now;
                _context.Add(orders);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm mới đơn hàng thành công!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers.OrderBy(c => c.FullName), "Id", "FullName", orders.CustomerId);
            return View(orders);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            ViewData["CustomerId"] = new SelectList(_context.Customers.OrderBy(c => c.FullName), "Id", "FullName", order.CustomerId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,Name,Email,Address,Status,CreatedDate")] Orders orders)
        {
            if (id != orders.Id) return NotFound();

            ModelState.Remove("Customer");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Cập nhật đơn hàng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Orders.AnyAsync(e => e.Id == orders.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers.OrderBy(c => c.FullName), "Id", "FullName", orders.CustomerId);
            return View(orders);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null) return NotFound();

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                // Xóa các chi tiết đơn hàng trước nếu có
                var details = await _context.OrderDetails.Where(od => od.OrderId == id).ToListAsync();
                if (details.Any())
                {
                    _context.OrderDetails.RemoveRange(details);
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Xóa đơn hàng thành công!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
