using LibraryManagementSystem.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.ViewModel
{
    public class IssueViewModel
    {
        public List<Customer>? Customers { get; set; }

        [DisplayName("Customer")]
        public int SelectedCustomer { get; set; }
        public List<Book>? Books { get; set; }

        [DisplayName("Book")]
        public int SelectedBook { get; set; }

    }
}
