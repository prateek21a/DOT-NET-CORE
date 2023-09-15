using System;
using System.ComponentModel.DataAnnotations;

namespace Login_Form.Models
{
    public enum UserRole
    {
        Admin,
        Manager,
        HR,
        Employee
    }

    public partial class UserTbl
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Age must be greater than zero.")]
        public int? Age { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"[A-Za-z\d!@#$%^&*()_+]{8,}$",
        ErrorMessage = "Password must be at least 8 characters ")]

        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; } 
    }
}
