using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningAPI.Models.Entities
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        public int UserId { get; set; } // FK to Users
        public int QuizId { get; set; } // FK to Quizzes

        public int Score { get; set; }

        public DateTime AttemptDate { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("QuizId")]
        public Quiz Quiz { get; set; }
    }
}