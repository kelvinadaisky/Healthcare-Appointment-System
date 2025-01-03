﻿
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_prog_Project.Models
{
    public class AssistantShift
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ShiftDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

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
