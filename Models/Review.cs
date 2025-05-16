namespace SmartTutionHub.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public required Course Course { get; set; }

        public int StudentId { get; set; }
        public required User Student { get; set; }

        public int Rating { get; set; } // 1-5

        public required string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
