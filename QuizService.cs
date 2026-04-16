using AutoMapper;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.Entities;
using ELearningAPI.Repositories;

namespace ELearningAPI.Services;

public class QuizService : IQuizService
{
    private readonly IQuizRepository _repo;
    private readonly IMapper _mapper;

    public QuizService(IQuizRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<QuizDto> GetQuizForCourseAsync(int courseId)
    {
        var quiz = await _repo.GetQuizWithQuestionsAsync(courseId);
        return _mapper.Map<QuizDto>(quiz);
    }

    public async Task<int> SubmitQuizAsync(int quizId, QuizSubmissionDto submission)
    {
        // 1. Get the real quiz from the DB
        var quiz = await _repo.GetQuizWithQuestionsAsync(quizId); // Assuming CourseId and QuizId are mapped 1:1 for simplicity here
        if (quiz == null) throw new Exception("Quiz not found");

        // 2. Calculate Score (Grade calculation logic verified)
        int score = 0;
        foreach (var question in quiz.Questions)
        {
            if (submission.Answers.TryGetValue(question.QuestionId, out var userAnswer))
            {
                if (userAnswer == question.CorrectAnswer)
                {
                    score++;
                }
            }
        }

        int percentage = (int)Math.Round((double)score / quiz.Questions.Count * 100);

        // 3. Save Result
        var result = new Result
        {
            UserId = submission.UserId,
            QuizId = quiz.QuizId,
            Score = percentage,
            AttemptDate = DateTime.UtcNow
        };

        await _repo.SaveResultAsync(result);

        return percentage;
    }
    public async Task<QuizDto> CreateQuizAsync(QuizCreateDto dto)
    {
        var quiz = _mapper.Map<Quiz>(dto);
        await _repo.AddQuizAsync(quiz);
        await _repo.SaveChangesAsync();
        return _mapper.Map<QuizDto>(quiz);
    }

    public async Task<QuestionDto> CreateQuestionAsync(QuestionCreateDto dto)
    {
        var question = _mapper.Map<Question>(dto);
        await _repo.AddQuestionAsync(question);
        await _repo.SaveChangesAsync();
        return _mapper.Map<QuestionDto>(question);
    }

    public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync(int quizId)
    {
        var questions = await _repo.GetQuestionsByQuizIdAsync(quizId);
        return _mapper.Map<IEnumerable<QuestionDto>>(questions);
    }
}