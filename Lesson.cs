using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningAPI.Models.Entities
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }

        public int CourseId { get; set; } // FK to Courses

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int OrderIndex { get; set; }

        // Navigation Properties
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}