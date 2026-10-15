using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Egypt_Ticket.Data;
using Smart_Egypt_Ticket.Models;

namespace Smart_Egypt_Ticket.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DestinationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Destinations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Destinations.ToListAsync());
        }

        // GET: Destinations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var destination = await _context.Destinations
                .Include(d => d.Monuments)
                    .ThenInclude(m => m.TicketTypes)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (destination == null) return NotFound();

            return View(destination);
        }

        // GET: Destinations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Destinations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,City,Description,ImageUrl")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(destination);
        }

        // GET: Destinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null) return NotFound();

            return View(destination);
        }

        // POST: Destinations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,Description,ImageUrl")] Destination destination)
        {
            if (id != destination.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Destinations.Any(e => e.Id == destination.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(destination);
        }

        // GET: Destinations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var destination = await _context.Destinations
                .FirstOrDefaultAsync(d => d.Id == id);

            if (destination == null) return NotFound();

            return View(destination);
        }

        // POST: Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination != null)
                _context.Destinations.Remove(destination);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}