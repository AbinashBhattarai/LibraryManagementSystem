using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Book_DAL _bookDAL;
        private readonly Customer_DAL _customerDAL;
        private readonly Lender_DAL _lenderDAL;

        public HomeController(ILogger<HomeController> logger, Book_DAL bookDAL, Customer_DAL customerDAL, Lender_DAL lenderDAL)
        {
            _logger = logger;
            _bookDAL = bookDAL;
            _customerDAL = customerDAL;
            _lenderDAL = lenderDAL;
        }

        public IActionResult Index()
        {
            try
            {
                var books = _bookDAL.GetAllBooks();
                var customers = _customerDAL.GetAllCustomers();
                var lenders = _lenderDAL.GetAllLenders();

                int defaulterCount = 0;
                int totalPenalty = 0;
                foreach (var lender in lenders)
                {
                    if (lender.ReturnDate < DateTime.Now)
                    {
                        TimeSpan duration = DateTime.Now - lender.ReturnDate;
                        double days = duration.TotalDays;
                        lender.Penalty = (int)days * 5;

                        defaulterCount++;
                        totalPenalty += lender.Penalty;

                        bool result = _lenderDAL.UpdateLender(lender);
                    }
                }
                ViewBag.bookCount = books.Count;
                ViewBag.customerCount = customers.Count;
                ViewBag.issueCount = lenders.Count;
                ViewBag.totalPenalty = totalPenalty;
                ViewBag.defaulterCount = defaulterCount;
                return View();
            }
            catch (Exception)
            {

                TempData["error"] = "Database exception occured";
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
