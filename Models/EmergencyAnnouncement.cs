using System.ComponentModel.DataAnnotations;

namespace Web_prog_Project.Models
{
    public class EmergencyAnnouncement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
