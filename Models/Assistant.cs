using System.ComponentModel.DataAnnotations;

namespace Web_prog_Project.Models
{
    public class Assistant
    {
        [Key]
        public int AssistantId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
