using SmartTutionHub.Models;
using System.ComponentModel.DataAnnotations;

namespace SmartTutionHub.ViewModels
{
    public class ProfileVm
    {
        public string FullName { get; set; }
        public string? ProfileImagePath { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public IEnumerable<Course> MyClasses { get; set; } = new List<Course>();
    }
}
