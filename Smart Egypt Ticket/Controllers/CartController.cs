using Microsoft.AspNetCore.Mvc;
using Smart_Egypt_Ticket.Data;
using Smart_Egypt_Ticket.Extensions;
using Smart_Egypt_Ticket.Models;
using Microsoft.EntityFrameworkCore;

namespace Smart_Egypt_Ticket.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "Cart";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        private Cart GetCart()
        {
            return HttpContext.Session.GetObject<Cart>(CartSessionKey) ?? new Cart();
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetObject(CartSessionKey, cart);
        }

        // GET: Cart
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // POST: Cart/Add
        [HttpPost]
        public async Task<IActionResult> Add(int ticketTypeId, int quantity, DateTime visitDate)
        {
            if (quantity < 1) quantity = 1;

            var ticketType = await _context.TicketTypes
               
                .Include(t => t.Monument)
                .FirstOrDefaultAsync(t => t.Id == ticketTypeId);

            if (ticketType == null) return NotFound();

            var cart = GetCart();

            var existing = cart.Items.FirstOrDefault(i =>
                i.TicketTypeId == ticketTypeId && i.VisitDate.Date == visitDate.Date);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    TicketTypeId = ticketType.Id,
                    TicketTypeName = ticketType.Name,
                    MonumentName = ticketType.Monument!.Name,
                    UnitPrice = ticketType.Price,
                    Quantity = quantity,
                    VisitDate = visitDate
                });
            }

            SaveCart(cart);
            TempData["Success"] = $"Added {quantity} x {ticketType.Name} to cart.";
            return RedirectToAction("Index", "Cart");
        }

        // POST: Cart/Remove
        [HttpPost]
        public IActionResult Remove(int ticketTypeId, DateTime visitDate)
        {
            var cart = GetCart();
            cart.Items.RemoveAll(i => i.TicketTypeId == ticketTypeId && i.VisitDate.Date == visitDate.Date);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // POST: Cart/Clear
        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction("Index");
        }
    }
}