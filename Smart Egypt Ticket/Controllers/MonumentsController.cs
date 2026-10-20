using Microsoft.AspNetCore.Mvc;
using Smart_Egypt_Ticket.Data;
using Smart_Egypt_Ticket.Models;

namespace Smart_Egypt_Ticket.Controllers
{
    public class MonumentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("DestinationId,Name,Description,ImageUrl,OpeningHours,IsActive")] Monument monument)
        {
            if (!ModelState.IsValid)
            {
                return View(monument);
            }

            monument.CreatedAtUtc = DateTime.UtcNow;
            _context.Monuments.Add(monument);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }

}



 

