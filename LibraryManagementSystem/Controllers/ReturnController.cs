using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class ReturnController : Controller
    {
        private readonly AppDbContext _context;
        public ReturnController(AppDbContext context)
        {
            _context = context;  
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetIssuedBooks(string customerCode)
        {
            if (!string.IsNullOrEmpty(customerCode))
            {
                var customerList = _context.Customer
                    .Where(c => c.Code == customerCode.ToUpper())
                    .ToList();
                if (customerList.Count > 0)
                {
                    var customerBooks = _context.Book
                     .Include(x => x.Lenders)
                     .Where(x => x.Lenders.Any(y => y.CustomerId == customerList[0].Id))
                     .ToList();
                    if (customerBooks.Count > 0) 
                    {
                        LenderViewModel lenderViewModel = new LenderViewModel();
                        lenderViewModel.Customer = customerList;
                        lenderViewModel.Books = customerBooks;
                        return View(lenderViewModel);
                    }
                    TempData["success"] = "Customer donot have any issued books";
                    return RedirectToAction("Index");
                }
                TempData["success"] = "Customer not found";
                return RedirectToAction("Index");
            }
            TempData["success"] = "Enter valid customer code";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckPenalty(int customer, int book)
        {
            var lender = _context.Lender
                .Where(x => x.CustomerId == customer && x.BookId == book)
                .ToList();

            var bookDetails = _context.Book
                .Where(x => x.Id == book)
                .ToList();
            var customerDetails = _context.Customer
                .Where(x => x.Id == customer)
                .ToList();

            LenderViewModel lenderViewModel = new LenderViewModel();
            lenderViewModel.Customer = customerDetails;
            lenderViewModel.Books = bookDetails;
            lenderViewModel.Penalty = lender[0].Penalty;
            return View(lenderViewModel);
        }

        [HttpPost]
        public IActionResult ReturnBook(int customer, int book)
        {
            var lender = _context.Lender
                .Where(x => x.CustomerId == customer && x.BookId == book)
                .ToList();

            var bookDetails = _context.Book
                .Where(b => b.Id == book)
                .ToList();
            var customerDetails = _context.Customer
                .Where(c => c.Id == customer)
                .ToList(); ;

            _context.Lender.Remove(lender[0]);
            bookDetails[0].Quantity = bookDetails[0].Quantity + 1;

            _context.SaveChanges();
            TempData["success"] = "Return Successfully";
            return RedirectToAction("GetIssuedBooks", new {customerCode = customerDetails[0].Code});
        }
    }
}
