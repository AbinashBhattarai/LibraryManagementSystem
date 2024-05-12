using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Lender
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public int Penalty { get; set; }
    }
}
