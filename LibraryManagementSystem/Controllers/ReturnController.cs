using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class ReturnController : Controller
    {
        private readonly Book_DAL _book;
        private readonly Customer_DAL _customer;
        private readonly Lender_DAL _dal;
        public ReturnController(Book_DAL book, Customer_DAL customer, Lender_DAL dal)
        {
            _book = book;
            _customer = customer;
            _dal = dal;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetCustomer()
        {
            try
            {
                var customers = _customer.GetAllCustomers();
                return new JsonResult(customers);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return new JsonResult("");
            }
        }

        [HttpGet]
        public JsonResult GetBookByCustomer(int id)
        {
            try
            {
                var books = _dal.GetBookByCustomer(id);
                return new JsonResult(books);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return new JsonResult("");
            }
        }


        [HttpGet]
        public JsonResult CheckPenalty(int customerId, int bookId)
        {
            try
            {
                var penalty = _dal.GetPenaltyById(customerId, bookId);
                return new JsonResult(penalty);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return new JsonResult("");
            }
        }

        [HttpPost]
        public IActionResult RemoveLender(int customer, int book)
        {
            if (customer > 0 && book > 0)
            {
                try
                {
                    bool result = _dal.DeleteLender(customer, book);
                    if (!result)
                    {
                        TempData["error"] = "Cannot return the book";
                        return RedirectToAction("Index");
                    }
                    TempData["success"] = "Book Returned successfully";
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
