using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningAPI.Models.Entities
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public int QuizId { get; set; } // FK to Quizzes

        [Required]
        public string QuestionText { get; set; }

        [Required] public string OptionA { get; set; }
        [Required] public string OptionB { get; set; }
        [Required] public string OptionC { get; set; }
        [Required] public string OptionD { get; set; }
        
        [Required] 
        public string CorrectAnswer { get; set; }

        // Navigation Properties
        [ForeignKey("QuizId")]
        public Quiz Quiz { get; set; }
    }
}