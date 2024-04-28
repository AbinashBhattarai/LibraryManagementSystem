using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var customerList = _context.Customer.ToList();
            var bookList = _context.Book.ToList();
            var lenderList = _context.Lender.ToList();
            int defaulterCount = 0;
            int totalPenalty = 0;
            foreach(var lender in lenderList)
            {
                if(lender.ToDate < DateTime.Now)
                {
                    TimeSpan duration = DateTime.Now - lender.ToDate;
                    double days = duration.TotalDays;
                    lender.Penalty = (int)days * 5;
                    _context.SaveChanges();
                    defaulterCount++;
                    totalPenalty += lender.Penalty;
                }
            }
            ViewData["customerCount"] = customerList.Count;
            ViewData["bookCount"] = bookList.Count;
            ViewData["IssueCount"] = lenderList.Count;
            ViewData["defaulterCount"] = defaulterCount;
            ViewData["totalPenalty"] = totalPenalty;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
