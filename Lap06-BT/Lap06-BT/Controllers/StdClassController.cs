using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lap06_BT.Entities;
using Lap06_BT.Models;

namespace Lap06_BT.Controllers
{
    public class StdClassController : Controller
    {
        private readonly AppDbContext _context;

        public StdClassController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StdClass
        public async Task<IActionResult> Index()
        {
            return View(await _context.StdClasses.ToListAsync());
        }

        // GET: StdClass/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stdclass = await _context.StdClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stdclass == null)
            {
                return NotFound();
            }

            return View(stdclass);
        }

        // GET: StdClass/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StdClass/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CLassName")] StdClass stdclass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stdclass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stdclass);
        }

        // GET: StdClass/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stdclass = await _context.StdClasses.FindAsync(id);
            if (stdclass == null)
            {
                return NotFound();
            }
            return View(stdclass);
        }

        // POST: StdClass/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CLassName")] StdClass stdclass)
        {
            if (id != stdclass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stdclass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StdClassExists(stdclass.Id))
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
            return View(stdclass);
        }

        // GET: StdClass/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stdclass = await _context.StdClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stdclass == null)
            {
                return NotFound();
            }

            return View(stdclass);
        }

        // POST: StdClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stdclass = await _context.StdClasses.FindAsync(id);
            if (stdclass != null)
            {
                _context.StdClasses.Remove(stdclass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StdClassExists(int id)
        {
            return _context.StdClasses.Any(e => e.Id == id);
        }
    }
}