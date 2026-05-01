using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.DTOs // Update this namespace to match your project!
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        // This regex perfectly matches the one we put in app.js!
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password is too weak.")]
        public string Password { get; set; }
    }
}