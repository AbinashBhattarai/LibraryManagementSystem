using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.ViewModel
{
    public class LenderViewModel
    {
        public int Id { get; set; }
        public List<Customer> Customer { get; set; }
        public List<Book> Books { get; set; }
        public Lender FromDate { get; set; }
        public Lender ToDate { get; set;}
        public int Penalty { get; set; }
    }
}
