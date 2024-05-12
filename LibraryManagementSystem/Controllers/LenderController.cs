using LibraryManagementSystem.DAL;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class LenderController : Controller
    {
        private readonly Lender_DAL _dal;
        public LenderController(Lender_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index(string code)
        {
            List<LenderViewModel> lenders = new List<LenderViewModel>();
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    lenders = _dal.GetLenderByCode(code);
                    return View(lenders);
                }
                lenders = _dal.GetAllLenders();
                return View(lenders);
            }
            catch
            {
                TempData["error"] = "Database exception occured";
                return View(lenders);
            }
        }
    }
}
