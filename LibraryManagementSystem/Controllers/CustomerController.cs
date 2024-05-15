using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly Customer_DAL _dal;
        public CustomerController(Customer_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index(string code)
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    customers = _dal.GetCustomerByCode(code);
                    return View(customers);
                }
                customers = _dal.GetAllCustomers();
                return View(customers);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return View(customers);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Code = GetNewCustomerCode();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _dal.AddCustomer(customer);
                    if (!result)
                    {
                        TempData["error"] = "Unable to add customer";
                        return View();
                    }
                    TempData["success"] = "Customer added successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["error"] = "Database exception occured";
                    return View();
                }
            }
            TempData["error"] = "Invalid data";
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Customer customer = _dal.GetCustomerById(id);
                if (customer.Id == 0)
                {
                    TempData["error"] = "Customer id not found";
                    return RedirectToAction("index");
                }
                return View(customer);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _dal.UpdateCustomer(customer);
                    if (!result)
                    {
                        TempData["error"] = "Unable to update customer";
                        return View();
                    }
                    TempData["success"] = "Customer updated successfully";
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
                Customer customer = _dal.GetCustomerById(id);
                if (customer.Id == 0)
                {
                    TempData["error"] = "Customer id not found";
                    return RedirectToAction("index");
                }
                return View(customer);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteBook(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = _dal.DeleteCustomer(customer.Id);
                    if (!result)
                    {
                        TempData["error"] = "Unable to delete customer";
                        return View();
                    }
                    TempData["success"] = "Customer deleted successfully";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["error"] = "Database exception occured";
                    return View();
                }
            }
            TempData["error"] = "Invalid data";
            return View();
        }

        private string GetNewCustomerCode()
        {
            string customerCode = "";
            try
            {
                string lastCode= _dal.GetLastCustomerCode();
                if (lastCode == null)
                {
                    customerCode = "cs001";
                }
                else
                {
                    int startIndex = 3;
                    int lastCustomerCode = int.Parse(lastCode.Substring(startIndex));
                    customerCode = $"cs{lastCustomerCode + 1:D3}";

                }
                return customerCode;
            }
            catch
            {
                return ((string)(TempData["error"] = "Database exception occured. Cannot generate code"));
            }
        }
    }
}
