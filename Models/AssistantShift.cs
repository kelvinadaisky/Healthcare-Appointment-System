using System;
using System.ComponentModel.DataAnnotations;

namespace Web_prog_Project.Models
{
    public class AssistantShift
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        // Foreign key for Department
        [Required]
        public int DepartmentId { get; set; }

        // Foreign key for Assistant
        [Required]
        public int AssistantId { get; set; }

        // Navigation properties
        public Department? Department { get; set; }
        public Assistant? Assistant { get; set; }
    }
}
