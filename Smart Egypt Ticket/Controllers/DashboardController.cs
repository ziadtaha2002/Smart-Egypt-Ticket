using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Egypt_Ticket.Data;

namespace Smart_Egypt_Ticket.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.DestinationCount = await _context.Destinations.CountAsync(item => item.IsActive);
            ViewBag.MonumentCount = await _context.Monuments.CountAsync(item => item.IsActive);
            ViewBag.TicketTypeCount = await _context.TicketTypes.CountAsync(item => item.IsActive);
            ViewBag.OrderCount = await _context.Orders.CountAsync();
            ViewBag.PaidOrderCount = await _context.Orders.CountAsync(order => order.Status == "Paid");
            ViewBag.Revenue = await _context.Orders
                .Where(order => order.Status == "Paid")
                .SumAsync(order => (decimal?)order.TotalAmount) ?? 0m;
            return View("Index");
        }

      

    }
}
