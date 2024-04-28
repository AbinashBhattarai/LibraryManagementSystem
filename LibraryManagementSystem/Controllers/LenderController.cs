using LibraryManagementSystem.Data;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class LenderController : Controller
    {
        private readonly AppDbContext _context;
        public LenderController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string customerCode)
        {
            var lenders = _context.Lender
                .Include(x => x.Customer)
                .Include(x => x.Book)
                .ToList();
            if (!string.IsNullOrEmpty(customerCode))
            {
                lenders = _context.Lender
                .Include(x => x.Customer)
                .Where(x => x.Customer.Code == customerCode)
                .Include(x => x.Book)
                .ToList();
                return View(lenders);
            }
            return View(lenders);
        }

        [HttpGet]
        public IActionResult GetDefaulter(string customerCode)
        {
            var defaulters = _context.Lender
                .Where(x => x.Penalty > 0)
                .Include(x => x.Customer)
                .Include(x => x.Book)
                .ToList();

            if (!string.IsNullOrEmpty(customerCode))
            {
                defaulters = _context.Lender
                .Include(x => x.Customer)
                .Where(x => x.Customer.Code == customerCode)
                .Include(x => x.Book)
                .Where(x => x.Penalty > 0)
                .ToList();
                return View(defaulters);
            }
            return View(defaulters);
        }
    }
}
