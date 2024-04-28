using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required, DisplayName("Customer Code")]
        public string? Code { get; set; }

        [Required, DisplayName("Full Name"), MaxLength(50)]
        public string? Name { get; set; }

        [Required, MaxLength(50)]
        public string? Email { get; set; }

        [Required ,MaxLength(50)]
        public string? Address { get; set; }

        [Required, MaxLength(10), DisplayName("Phone Number")]
        public string? Phone { get; set; }
        public virtual List<Lender> Lenders { get; set; } = new List<Lender>();
    }
}
