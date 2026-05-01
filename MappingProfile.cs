using AutoMapper;
using ELearningAPI.Models.Entities;
using ELearningAPI.Models.DTOs;

namespace ELearningAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Source -> Destination
        CreateMap<User, UserDto>();
        CreateMap<UserRegisterDto, User>();

        CreateMap<Course, CourseDto>();
        CreateMap<CourseCreateDto, Course>();

        CreateMap<Lesson, LessonDto>();
        CreateMap<Quiz, QuizDto>();
        CreateMap<Question, QuestionDto>();

        CreateMap<UserUpdateDto, User>();
        CreateMap<CourseUpdateDto, Course>();
        CreateMap<LessonCreateDto, Lesson>();
        CreateMap<LessonUpdateDto, Lesson>();
        CreateMap<QuizCreateDto, Quiz>();
        CreateMap<QuestionCreateDto, Question>();
        CreateMap<Result, ResultDto>();
    }
}