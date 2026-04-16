using AutoMapper;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.Entities;
using ELearningAPI.Repositories;

namespace ELearningAPI.Services;

public interface IQuizService
{
    Task<QuizDto> GetQuizForCourseAsync(int courseId);
    Task<int> SubmitQuizAsync(int quizId, QuizSubmissionDto submission);
    Task<QuizDto> CreateQuizAsync(QuizCreateDto dto);
    Task<QuestionDto> CreateQuestionAsync(QuestionCreateDto dto);
    Task<IEnumerable<QuestionDto>> GetQuestionsAsync(int quizId);
}

