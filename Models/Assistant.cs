using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public string Email { get; set; }
        public ICollection<AssistantShift> AssistantShifts { get; set; } = new List<AssistantShift>();


    }
}
