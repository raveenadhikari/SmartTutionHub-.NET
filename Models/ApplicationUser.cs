using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmartTutionHub.Models
{
    public class ApplicationUser : IdentityUser<int> //  You're using int IDs
    {
        [Required]
        public required string FullName { get; set; }

        [Required]
        public required string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public required string NIC { get; set; }

        [Required]
        public required string Role { get; set; } // Student or Tutor

        public string? ProfileImagePath { get; set; }
    }
}
