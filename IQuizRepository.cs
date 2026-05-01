using ELearningAPI.Data;
using ELearningAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Repositories;

public interface IQuizRepository
{
    Task<Quiz> GetQuizWithQuestionsAsync(int courseId);
    Task<Result> SaveResultAsync(Result result);
    Task AddQuizAsync(Quiz quiz);
    Task AddQuestionAsync(Question question);
    Task<IEnumerable<Question>> GetQuestionsByQuizIdAsync(int quizId);
    Task SaveChangesAsync();
}
