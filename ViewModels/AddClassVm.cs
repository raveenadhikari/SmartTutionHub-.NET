using System.ComponentModel.DataAnnotations;

namespace SmartTutionHub.ViewModels
{
    public class AddClassVm
    {
        [Required]
        public string Level { get; set; } // Grade 5, O/L, A/L
        [Required]
        public string TutorName { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string GradeOrYear { get; set; }
        [Required]
        public string Medium { get; set; } // English, Sinhala, Tamil
        [Required]
        public string ClassType { get; set; } // Individual, Hall, Group
        [Required]
        public DateTime ClassTime { get; set; }
        public bool ToBeDiscussed { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        public IFormFile Thumbnail { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
