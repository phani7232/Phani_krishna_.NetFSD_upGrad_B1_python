using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.Models.DTOs;

public class QuizDto
{
    public int QuizId { get; set; }
    public string Title { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}

public class QuestionDto
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string OptionA { get; set; }
    public string OptionB { get; set; }
    public string OptionC { get; set; }
    public string OptionD { get; set; }
    // Notice: We DO NOT send the CorrectAnswer to the frontend! No cheating!
}

public class QuizSubmissionDto
{
    public int UserId { get; set; }
    public Dictionary<int, string> Answers { get; set; } = new(); // QuestionId, SelectedOption
}
public class QuizCreateDto
{
    [Required]
    public int CourseId { get; set; }
    
    [Required, MaxLength(200)]
    public string Title { get; set; }
}

public class QuestionCreateDto
{
    [Required] public int QuizId { get; set; }
    [Required] public string QuestionText { get; set; }
    [Required] public string OptionA { get; set; }
    [Required] public string OptionB { get; set; }
    [Required] public string OptionC { get; set; }
    [Required] public string OptionD { get; set; }
    [Required] public string CorrectAnswer { get; set; }
}

public class ResultDto
{
    public int ResultId { get; set; }
    public int QuizId { get; set; }
    public int Score { get; set; }
    public DateTime AttemptDate { get; set; }
}