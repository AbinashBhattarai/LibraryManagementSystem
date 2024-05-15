using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class ReturnController : Controller
    {
        private readonly Book_DAL _bookDAL;
        private readonly Customer_DAL _customerDAL;
        private readonly Lender_DAL _lenderDAL;
        public ReturnController(Book_DAL bookDAL, Customer_DAL customerDAL, Lender_DAL lenderDAL)
        {
            _bookDAL = bookDAL;
            _customerDAL = customerDAL;
            _lenderDAL = lenderDAL;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetCustomer()
        {
            try
            {
                var customers = _customerDAL.GetAllCustomers();
                return new JsonResult(customers);
            }
            catch
            {
                return new JsonResult("");
            }
        }

        [HttpGet]
        public JsonResult GetBooksByLender(int id)
        {
            try
            {
                var books = _lenderDAL.GetBooksByLender(id);
                return new JsonResult(books);
            }
            catch
            {
                return new JsonResult ("");
            }
        }


        [HttpGet]
        public JsonResult CheckPenalty(int customerId, int bookId)
        {
            try
            {
                var penalty = _lenderDAL.GetPenaltyById(customerId, bookId);
                return new JsonResult(penalty);
            }
            catch
            {
                return new JsonResult("");
            }
        }

        [HttpPost]
        public IActionResult RemoveLender(IssueViewModel issueViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Lender lender = new Lender();
                    lender.CustomerId = issueViewModel.SelectedCustomer;
                    lender.BookId = issueViewModel.SelectedBook;
                    bool result = _lenderDAL.DeleteLender(lender);
                    if (result)
                    {
                        var book = _bookDAL.GetBookById(issueViewModel.SelectedBook);
                        book.Quantity = (book.Quantity) + 1;
                        bool QuantityUpdate = _bookDAL.UpdateBook(book);

                        TempData["success"] = "Book Returned successfully";
                        return RedirectToAction("Index");
                    }
                    TempData["error"] = "Cannot return the book";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["error"] = "Database exception occured";
                    return RedirectToAction("Index");
                }
            }
            TempData["error"] = "Invalid data";
            return RedirectToAction("Index");
        }
    }
}
