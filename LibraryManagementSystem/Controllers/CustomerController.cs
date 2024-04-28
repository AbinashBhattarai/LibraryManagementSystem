using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string code)
        {
            List<Customer> customerList = _context.Customer.ToList();
            if (!string.IsNullOrEmpty(code))
            {
                customerList = customerList.Where(c => c.Code == code.ToUpper()).ToList();
            }
            return View(customerList);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            string customerCode = GetNewCustomerCode();
            ViewData["customerCode"] = customerCode;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if(ModelState.IsValid)
            {
                _context.Customer.Add(customer);
                _context.SaveChanges();
                TempData["success"] = "Added Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return RedirectToAction("Error", "Home");
            }
            Customer? customer = _context.Customer.FirstOrDefault(c => c.Id == id);
            if(customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customer.Update(customer);
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
            Customer? customer = _context.Customer.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Customer? customer = _context.Customer.Find(id);
            var bookList = _context.Book
                    .Include(x => x.Lenders)
                    .Where(x => x.Lenders.Any(y => y.CustomerId == id))
                    .ToList();
            if (customer == null)
            {
                return NotFound();
            }
            if(bookList.Count > 0)
            {
                TempData["success"] = "Cannot delete customer. Please return the books first";
                return RedirectToAction("Index");
            }
            _context.Customer.Remove(customer);
            _context.SaveChanges();
            TempData["success"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        private string GetNewCustomerCode()
        {
            var lastCustomer = _context.Customer
                                    .OrderByDescending(c => c.Code)
                                    .FirstOrDefault();
            string customerCode = "";
            if (lastCustomer == null)
            {
                customerCode = "CS001";
            }
            else
            {
                int startIndex = 3;
                int lastCode = int.Parse(lastCustomer.Code.Substring(startIndex));
                customerCode = $"CS{lastCode + 1:D3}";

            }
            return customerCode;
        }
    }
}
