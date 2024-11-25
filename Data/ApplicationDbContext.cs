using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_prog_Project.Models;

namespace Web_prog_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);  // Logs queries to the console
        }

        // DbSet properties should be at the class level
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<FacultyMember> FacultyMembers { get; set; }
        public DbSet<Department> Departments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Assistants
            modelBuilder.Entity<Assistant>().HasData(
                new Assistant
                {
                    AssistantId = 1,
                    Email = "kelvin@gmail.com",
                    FirstName = "Kelvin",
                    LastName = "Irutingabo",
                    Phone = "+257 77492508",
                    Specialization = "Kids"
                },
                new Assistant
                {
                    AssistantId = 2,
                    Email = "kelvin2002@gmail.com",
                    FirstName = "Joujou",
                    LastName = "Ndayizeye",
                    Phone = "+257 77730599",
                    Specialization = "baby"
                }
            );

            modelBuilder.Entity<FacultyMember>()
                .HasOne(f => f.Department)
                .WithMany()
                .HasForeignKey(f => f.DepartmentId);
        }
    }
}
