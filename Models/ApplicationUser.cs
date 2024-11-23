using Microsoft.AspNetCore.Identity;

namespace Web_prog_Project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}