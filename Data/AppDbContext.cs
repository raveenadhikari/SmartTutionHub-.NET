using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartTutionHub.Models;

namespace SmartTutionHub.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "Student", NormalizedName = "STUDENT" },
                new ApplicationRole { Id = 2, Name = "Tutor", NormalizedName = "TUTOR" }
             );


            modelBuilder.Entity<Course>()
                .HasOne(c => c.Tutor)
                .WithMany()
                .HasForeignKey(c => c.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
