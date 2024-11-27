﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_prog_Project.Models
{
    public class FacultyMember
    {
        [Key]
        public int FacultyMemberId { get; set; }

        [Required]
        [DisplayName("First Name")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Phone]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [EmailAddress]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        // Foreign Key for Department
        [Required]
        public int DepartmentId { get; set; }
    }
}
