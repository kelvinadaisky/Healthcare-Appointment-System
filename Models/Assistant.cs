using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_prog_Project.Models
{
    public class Assistant
    {
        [Key]
        public int AssistantId { get; set; }
        [Required]
        [DisplayName("First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }
        public string Phone { get; set; }
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [ForeignKey("Email")]
        public ApplicationUser? User { get; set; } // Navigation property

        public ICollection<AssistantShift> AssistantShifts { get; set; } = new List<AssistantShift>();


    }
}