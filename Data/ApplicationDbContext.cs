using Microsoft.AspNetCore.Identity;
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

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Assistant> Assistants { get; set; }
        public DbSet<FacultyMember> FacultyMembers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AssistantShift> AssistantShifts { get; set; }
        public DbSet<EmergencyAnnouncement> EmergencyAnnouncements { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<FacultyMember>()
                .HasOne<Department>()
                .WithMany()
                .HasForeignKey(f => f.DepartmentId);

            // Configure Shift and Assistant relationship
            modelBuilder.Entity<AssistantShift>()
                .HasOne(s => s.Assistant) // Use the navigation property
                .WithMany(a => a.AssistantShifts) // Assuming you have a collection in Assistant
                .HasForeignKey(s => s.AssistantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Shift and  Department relationship
            modelBuilder.Entity<AssistantShift>()
                .HasOne(s => s.Department) // Use the navigation property
                .WithMany(d => d.AssistantShifts) // Assuming you have a collection in Department
                .HasForeignKey(s => s.DepartmentId);

            modelBuilder.Entity<Appointment>()
                  .HasOne(a => a.Doctor)
                  .WithMany() // If ApplicationUser has no navigation property for appointments
                  .HasForeignKey(a => a.DoctorId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assistant>()
                .HasOne(a => a.User) // Navigation property in Assistant
                .WithMany() // IdentityUser does not have a collection of Assistants
                .HasForeignKey(a => a.Email) // Use Email as the foreign key
                .HasPrincipalKey(u => u.Email) // IdentityUser's Email as principal key
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FacultyMember>()
               .HasOne(a => a.User) // Navigation property in Assistant
               .WithMany() 
               .HasForeignKey(a => a.Email) // Use Email as the foreign key
               .HasPrincipalKey(u => u.Email) // IdentityUser's Email as principal key
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
