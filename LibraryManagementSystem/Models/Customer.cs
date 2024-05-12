using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string? Phone { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}
