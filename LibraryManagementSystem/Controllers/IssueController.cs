using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystem.Controllers
{
    public class IssueController : Controller
    {
        private readonly Book_DAL _bookDAL;
        private readonly Customer_DAL _customerDAL;
        private readonly Lender_DAL _lenderDAL;
        public IssueController(Book_DAL bookDAL, Customer_DAL customerDAL, Lender_DAL lenderDAL)
        {
            _bookDAL = bookDAL;
            _customerDAL = customerDAL;
            _lenderDAL = lenderDAL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IssueViewModel issueViewModel = new IssueViewModel();
            try
            {
                issueViewModel.Books = _bookDAL.GetAllBooks();
                issueViewModel.Customers = _customerDAL.GetAllCustomers();
                return View(issueViewModel);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return View(issueViewModel);
            }
        }

        [HttpPost]
        public IActionResult CreateIssue(IssueViewModel issueViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var book = _bookDAL.GetBookById(issueViewModel.SelectedBook);
                    if (book.Quantity > 0)
                    {
                        Lender lender = new Lender();
                        lender.CustomerId = issueViewModel.SelectedCustomer;
                        lender.BookId = issueViewModel.SelectedBook;
                        lender.IssueDate = DateTime.Now;
                        lender.ReturnDate = lender.IssueDate.AddDays(14);
                        lender.Penalty = 0;
                        bool result = _lenderDAL.AddLender(lender);
                        if (result)
                        {
                            book.Quantity = (book.Quantity) - 1;
                            bool QuantityUpdate = _bookDAL.UpdateBook(book);
                            TempData["success"] = "Book issued successfully";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["error"] = "Book already issued. No duplicate issue allowed";
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["error"] = "Book out of stock";
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    TempData["error"] = "Database exception occured";
                    return RedirectToAction("Index");
                }
            }
            TempData["error"] = "Please select customer and book";
            return RedirectToAction("Index");
        }
    }
}
