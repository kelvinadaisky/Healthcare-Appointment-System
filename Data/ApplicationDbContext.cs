using Microsoft.EntityFrameworkCore;
using Web_prog_Project.Models;

namespace Web_prog_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Assistant> Assistants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assistant>().HasData(
                new Assistant { AssistantId = 1, Email = "kelvin@gmail.com", FirstName = "Kelvin", LastName = "Irutingabo", Phone = "+257 77492508", Specialization = "Kids" },
                new Assistant { AssistantId = 2, Email = "kelvin2002@gmail.com", FirstName = "Joujou", LastName = "Ndayizeye", Phone = "+257 77730599", Specialization = "baby" }
                );
        }


    }
}
