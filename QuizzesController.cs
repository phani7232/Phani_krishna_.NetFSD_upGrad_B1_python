using ELearningAPI.Models.DTOs;
using ELearningAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELearningAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizzesController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet("{courseId}")]
    public async Task<IActionResult> GetQuiz(int courseId)
    {
        var quiz = await _quizService.GetQuizForCourseAsync(courseId);
        if (quiz == null) return NotFound();
        return Ok(quiz);
    }

    [HttpPost("{quizId}/submit")]
    public async Task<IActionResult> SubmitQuiz(int quizId, [FromBody] QuizSubmissionDto submission)
    {
        try
        {
            var score = await _quizService.SubmitQuizAsync(quizId, submission);
            return Ok(new { Score = score, Message = "Quiz submitted successfully!" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    [Route("/api/quizzes")]
    public async Task<IActionResult> CreateQuiz([FromBody] QuizCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var quiz = await _quizService.CreateQuizAsync(dto);
        return Created($"/api/quizzes/{quiz.QuizId}", quiz);
    }

    [HttpGet]
    [Route("/api/quizzes/{quizId}/questions")]
    public async Task<IActionResult> GetQuestions(int quizId)
    {
        var questions = await _quizService.GetQuestionsAsync(quizId);
        return Ok(questions);
    }

    [HttpPost]
    [Route("/api/questions")]
    public async Task<IActionResult> CreateQuestion([FromBody] QuestionCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var question = await _quizService.CreateQuestionAsync(dto);
        return Created($"/api/questions/{question.QuestionId}", question);
    }
}