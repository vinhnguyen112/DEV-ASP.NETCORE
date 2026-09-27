
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lap06_BT.Entities;

public class SubjectsController : Controller
{
    private readonly AppDbContext _context;

    public SubjectsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: SUBJECTSS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Subjects.ToListAsync());
    }

    // GET: SUBJECTSS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subjects = await _context.Subjects
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subjects == null)
        {
            return NotFound();
        }

        return View(subjects);
    }

    // GET: SUBJECTSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SUBJECTSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,SubjectName")] Subjects subjects)
    {
        if (ModelState.IsValid)
        {
            _context.Add(subjects);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(subjects);
    }

    // GET: SUBJECTSS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subjects = await _context.Subjects.FindAsync(id);
        if (subjects == null)
        {
            return NotFound();
        }
        return View(subjects);
    }

    // POST: SUBJECTSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,SubjectName")] Subjects subjects)
    {
        if (id != subjects.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(subjects);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectsExists(subjects.Id))
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
        return View(subjects);
    }

    // GET: SUBJECTSS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subjects = await _context.Subjects
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subjects == null)
        {
            return NotFound();
        }

        return View(subjects);
    }

    // POST: SUBJECTSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var subjects = await _context.Subjects.FindAsync(id);
        if (subjects != null)
        {
            _context.Subjects.Remove(subjects);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SubjectsExists(int? id)
    {
        return _context.Subjects.Any(e => e.Id == id);
    }
}
