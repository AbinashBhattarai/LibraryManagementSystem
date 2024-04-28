using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required, DisplayName("Book Code")]
        public string? Code { get; set; }

        [Required, MaxLength(50)]
        public string? ISBN { get; set; }

        [Required, MaxLength(50)]
        [DisplayName("Book Title")]
        public string? Title { get; set; }

        [Required, MaxLength(50)]
        public string? Author { get; set; }

        [Required, MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual List<Lender>? Lenders { get; set; }
    }
}
