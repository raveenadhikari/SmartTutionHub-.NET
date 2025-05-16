using System.ComponentModel.DataAnnotations;

namespace SmartTutionHub.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string FullName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public required string Role { get; set; } // "Student" or "Tutor"

        public ICollection<Course>? Courses { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
