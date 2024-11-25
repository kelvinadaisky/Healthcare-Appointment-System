using System.ComponentModel;
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

        public string Department { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [DisplayName("Office Hours")]
        public string OfficeHours { get; set; }
    }
}
