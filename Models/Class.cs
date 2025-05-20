using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SmartTutionHub.Models
{
    public class Class
    {
        public int Id { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public string TutorName { get; set; }
        [Required]
        public string Qualification { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string GradeOrYear { get; set; }
        [Required]
        public string Medium { get; set; }
        [Required]
        public string ClassType { get; set; }
        [Required]
        public DateTime ClassTime { get; set; }
        public bool ToBeDiscussed { get; set; }
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        public string ThumbnailPath { get; set; }
        public string GalleryPaths { get; set; } // Comma-separated image paths
        public int TutorId { get; set; }
        public ApplicationUser Tutor { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
