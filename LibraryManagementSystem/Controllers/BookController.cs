using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly Book_DAL _dal;
        public BookController(Book_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index(string isbn)
        {
            List<Book> books = new List<Book>();
            try
            {
                if (!string.IsNullOrEmpty(isbn))
                {
                    books = _dal.GetBookByISBN(isbn);
                    return View(books);
                }
                books = _dal.GetAllBooks();
                return View(books);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return View(books);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _dal.AddBook(book);
                    if(!result)
                    {
                        TempData["error"] = "Unable to add book";
                        return View();
                    }
                    TempData["success"] = "Book added successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["error"] = "Database exception occured";
                    return View();
                }
            }
            TempData["error"] = "Model state invalid";
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Book book = _dal.GetBookById(id);
                if(book.Id == 0)
                {
                    TempData["error"] = $"Book id not found";
                    return RedirectToAction("index");
                }
                return View(book);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return View() ;
            }
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _dal.UpdateBook(book);
                    if (!result)
                    {
                        TempData["error"] = "Unable to update book";
                        return View();
                    }
                    TempData["success"] = "Book updated successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["error"] = "Database exception occured";
                    return View();
                }
            }
            TempData["error"] = "Model state invalid";
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Book book = _dal.GetBookById(id);
                if (book.Id == 0)
                {
                    TempData["error"] = $"Book id not found";
                    return RedirectToAction("index");
                }
                return View(book);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteBook(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _dal.DeleteBook(book.Id);
                    if (!result)
                    {
                        TempData["error"] = "Unable to delete book";
                        return View();
                    }
                    TempData["success"] = "Book deleted successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["error"] = "Database exception occured";
                    return View();
                }
            }
            TempData["error"] = "Model state invalid";
            return View();
        }

    }
}
