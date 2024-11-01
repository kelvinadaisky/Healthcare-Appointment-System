using System.ComponentModel.DataAnnotations;

namespace Web_prog_Project.Models
{
    public class Users
    {
        [Key]
        public int AccountId { get; set; }         // Primary key
        [Required]
        public string Username { get; set; }       // Unique username
        public string Email { get; set; }          // User email, unique
        public string PasswordHash { get; set; }   // Password hash for security
        public DateTime CreatedAt { get; set; }    // Account creation date
        public bool IsActive { get; set; }         // Status of account
    }
}
