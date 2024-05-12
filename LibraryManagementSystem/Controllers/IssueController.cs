using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Controllers
{
    public class IssueController : Controller
    {
        private readonly Book_DAL _book;
        private readonly Customer_DAL _customer;
        private readonly Lender_DAL _dal;
        public IssueController(Book_DAL book, Customer_DAL customer, Lender_DAL dal)
        {
            _book = book;
            _customer = customer;
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IssueViewModel issueViewModel = new IssueViewModel();
            try
            {
                issueViewModel.Books = _book.GetAllBooks();
                issueViewModel.Customers = _customer.GetAllCustomers();
                return View(issueViewModel);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return View(issueViewModel);
            }
        }

        [HttpPost]
        public IActionResult CreateIssue(int customer, int book)
        {
            if(customer > 0 && book > 0)
            {
                Lender lender = new Lender();
                try
                {
                    lender.CustomerId = customer;
                    lender.BookId = book;
                    lender.IssueDate = DateTime.Now;
                    lender.ReturnDate = lender.IssueDate.AddDays(14);
                    lender.Penalty = 0;
                    bool result = _dal.AddLender(lender);
                    if (!result)
                    {
                        TempData["error"] = "Book already issued. No duplicate issue allowed";
                        return RedirectToAction("Index");
                    }
                    TempData["success"] = "Book issued successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["error"] = "Database exception occured";
                    return RedirectToAction("Index");
                }
            }
            TempData["error"] = "Not found";
            return RedirectToAction("Index");
        }
    }
}
