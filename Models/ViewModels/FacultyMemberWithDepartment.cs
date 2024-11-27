using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web_prog_Project.Models.ViewModels
{
    public class FacultyMemberWithDepartment
    {
        public int FacultyMemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }  // Store department name here

        public string DepartmentName { get; set; }  // Store department name here


    }
}
