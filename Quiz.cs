using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningAPI.Models.Entities
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        public int CourseId { get; set; } // FK to Courses

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        // Navigation Properties
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        
        public ICollection<Question> Questions { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}