using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Lender
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required, DisplayName("Issue Date")]
        public DateTime FromDate { get; set; }
        [DisplayName("Return Date")]
        public DateTime ToDate { get; set; }

        [Required]
        public int Penalty { get; set; }
    }
}
