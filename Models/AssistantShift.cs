
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_prog_Project.Models
{
    public class AssistantShift
    {
        [Key]
        public int Id { get; set; }

        // Date of the shift (e.g., 2024-12-07)
        [Required]
        [DataType(DataType.Date)]
        public DateTime ShiftDate { get; set; }

        // Time the shift starts (e.g., 08:00 AM)
        [Required]
        public TimeSpan StartTime { get; set; }

        // Time the shift ends (e.g., 04:00 PM)
        [Required]
        public TimeSpan EndTime { get; set; }

        // Foreign key for Department
        [Required]
        public int DepartmentId { get; set; }

        // Foreign key for Assistant
        [Required]
        public int AssistantId { get; set; }

        // Navigation properties
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }

        [ForeignKey("AssistantId")]
        public Assistant? Assistant { get; set; }
    }
}
