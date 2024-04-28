using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string code)
        {
            List<Book> bookList = _context.Book.ToList();
            if (!string.IsNullOrEmpty(code))
            {
                bookList = bookList.Where(b => b.Code == code.ToUpper()).ToList();
            }
            return View(bookList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            string bookCode = GetNewBookCode();
            ViewData["bookCode"] = bookCode;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if(ModelState.IsValid)
            {
                _context.Book.Add(book);
                _context.SaveChanges();
                TempData["success"] = "Added Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Error", "Home");
            }
            Book? book = _context.Book.FirstOrDefault(b => b.Id == id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Book.Update(book);
                _context.SaveChanges();
                TempData["success"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Error", "Home");
            }
            Book? book = _context.Book.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Book? book = _context.Book.Find(id);
            var customerList = _context.Customer
                    .Include(x => x.Lenders)
                    .Where(x => x.Lenders.Any(y => y.BookId == id))
                    .ToList();
            if (book == null)
            {
                return NotFound();
            }
            if(customerList.Count > 0)
            {
                TempData["success"] = "Cannot delete book. Please return the books first";
                return RedirectToAction("Index");
            }
            _context.Book.Remove(book);
            _context.SaveChanges();
            TempData["success"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        private string GetNewBookCode()
        {
            var lastBook = _context.Book
                                    .OrderByDescending(b => b.Code)
                                    .FirstOrDefault();
            string bookCode = "";
            if (lastBook == null)
            {
                bookCode = "BK001";
            }
            else
            {
                int startIndex = 3;
                int lastCode = int.Parse(lastBook.Code.Substring(startIndex));
                bookCode = $"BK{lastCode + 1:D3}";

            }
            return bookCode;
        }
    }
}
