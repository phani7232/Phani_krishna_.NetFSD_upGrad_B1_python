using ELearningAPI.Data;
using ELearningAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly ApplicationDbContext _context;

    public QuizRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Quiz> GetQuizWithQuestionsAsync(int courseId)
    {
        return await _context.Quizzes
            .Include(q => q.Questions)
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.CourseId == courseId);
    }

    public async Task<Result> SaveResultAsync(Result result)
    {
        await _context.Results.AddAsync(result);
        await _context.SaveChangesAsync();
        return result;
    }
    public async Task AddQuizAsync(Quiz quiz)
    {
        await _context.Quizzes.AddAsync(quiz);
    }

    public async Task AddQuestionAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
    }

    public async Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId)
    {
        return await _context.Questions
            .Where(q => q.QuizId == quizId)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}