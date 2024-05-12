using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModel
{
    public class IssueViewModel
    {
        public List<Book>? Books { get; set; }
        public List<Customer>? Customers { get; set; }
    }
}
