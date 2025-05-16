namespace SmartTutionHub.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public required Course Course { get; set; }

        public int StudentId { get; set; }
        public required User Student { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        public DateTime ScheduledDate { get; set; }

        public string Status { get; set; } = "Pending"; // or Confirmed, Cancelled
    }
}
