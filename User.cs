using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningAPI.Models.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Fulfills "PK, INT, Identity"
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)] // Maps to VARCHAR
        public string FullName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } // We will enforce UNIQUE in DbContext

        [Required]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties (Relationships)
        public ICollection<Course> CreatedCourses { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}