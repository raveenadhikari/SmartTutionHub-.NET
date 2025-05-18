using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartTutionHub.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public required string Subject { get; set; }

        public required string Description { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public int TutorId { get; set; }

        [ForeignKey("TutorId")]
        public required ApplicationUser Tutor { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
