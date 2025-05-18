using System;
using System.ComponentModel.DataAnnotations;

namespace SmartTutionHub.ViewModels
{
    public class RegisterVm
    {
        [Required] public string FullName { get; set; }
        [Required] public string Gender { get; set; }
        [Required, DataType(DataType.Date)] public DateTime DateOfBirth { get; set; }
        [Required] public string NIC { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required, Phone] public string Phone { get; set; }
        [Required] public string UserType { get; set; } // “Student” or “Tutor”

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
