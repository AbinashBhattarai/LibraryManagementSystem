using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Runtime.InteropServices;

namespace LibraryManagementSystem.Controllers
{
    public class IssueController : Controller
    {
        private readonly AppDbContext _context;

        public IssueController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetBooks(string customerCode) 
        {
            if (!string.IsNullOrEmpty(customerCode))
            {
                var customerList = _context.Customer
                    .Where(c => c.Code == customerCode.ToUpper())
                    .ToList();
                if(customerList.Count > 0) 
                {
                    LenderViewModel lenderViewModel = new LenderViewModel();
                    lenderViewModel.Customer = customerList;
                    return View(lenderViewModel);
                }
                TempData["success"] = "Customer not found";
                return RedirectToAction("Index");
            }
            TempData["success"] = "Enter valid customer code";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult IssueBook(int customer, string bookCode)
        {
            var customerList = _context.Customer
                    .Where(c => c.Id == customer)
                    .ToList();
            var code = customerList[0].Code;
            if (!string.IsNullOrEmpty(bookCode))
            {
                var bookList = _context.Book
                    .Where(b => b.Code == bookCode.ToUpper())
                    .ToList();
                if (bookList.Count > 0)
                {
                    if (bookList[0].Quantity > 0)
                    {
                        LenderViewModel lenderViewModel = new LenderViewModel();
                        lenderViewModel.Books = bookList;
                        lenderViewModel.Customer = customerList;
                        return View(lenderViewModel);
                    }
                    TempData["success"] = "Book out of stock";
                    return RedirectToAction("GetBooks", new {customerCode = code});
                }
                TempData["success"] = "Book not found";
                return RedirectToAction("GetBooks", new { customerCode = code });
            }
            TempData["success"] = "Enter valid Book code";
            return RedirectToAction("GetBooks", new { customerCode = code });
        }

        [HttpPost]
        public IActionResult IssueBook(int customer, int book, DateTime issueDate)
        {
            var bookDetails = _context.Book.Find(book);
            var customerDetails = _context.Customer.Find(customer);
            if (issueDate.Date == DateTime.Now.Date)
            {
                var checkBook = _context.Lender
                    .Where(x => x.CustomerId == customer && x.BookId == book)
                    .ToList();
                if (checkBook.Count == 0)
                {
                    Lender lender = new Lender();
                    lender.CustomerId = customer;
                    lender.BookId = book;
                    lender.FromDate = issueDate;
                    lender.ToDate = issueDate.AddDays(14);
                    lender.Penalty = 0;

                    _context.Lender.Add(lender);
                    bookDetails.Quantity = bookDetails.Quantity - 1;
                    _context.SaveChanges();

                    TempData["success"] = "Issued Successfully";
                    return RedirectToAction("GetBooks", new { customerCode = customerDetails?.Code });
                }
                TempData["success"] = "Customer aleady has this book issued. Cannot issue duplicate book";
                return RedirectToAction("GetBooks", new { customerCode = customerDetails?.Code });
            }
            TempData["success"] = "Please select a valid date";
            return RedirectToAction("IssueBook", new { customer = customer, bookCode = bookDetails?.Code });
        }
    }
}
