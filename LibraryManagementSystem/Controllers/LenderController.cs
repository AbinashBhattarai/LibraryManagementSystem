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
        public IActionResult Index()
        {
            var lenders = _context.Lender.ToList();
            LenderViewModel lenderViewModel = new LenderViewModel();
            return View(lenders);
        }

        [HttpGet]
        public IActionResult GetDefaulter()
        {
            var defaulters = _context.Lender
                .Where(x => x.Penalty > 0)
                .ToList();
            return View(defaulters);
        }
    }
}
