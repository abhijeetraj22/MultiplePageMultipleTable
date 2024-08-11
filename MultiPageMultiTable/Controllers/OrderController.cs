using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiPageMultiTable.Data;
using MultiPageMultiTable.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MultiPageMultiTable.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = _context.Orders.Include(o => o.Customer);
            return View(await orders.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            // Log the value of CustomerId
            Console.WriteLine($"CustomerId: {order.CustomerId}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model State Errors:");
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }

                ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name", order.CustomerId);
                return View(order);
            }

            if (order.CustomerId <= 0)
            {
                ModelState.AddModelError("CustomerId", "The Customer field is required.");
                ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "Name", order.CustomerId);
                return View(order);
            }

            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
