using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModel
{
    public class LenderViewModel : Lender
    {
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string? ISBN { get; set; }
        public string? Title { get; set; }
    }
}
