using AutoMapper;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.Entities;
using ELearningAPI.Repositories;

namespace ELearningAPI.Services;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
    Task<CourseDto> CreateCourseAsync(CourseCreateDto dto);
    Task<CourseDto> GetCourseByIdAsync(int id);
    Task<CourseDto> UpdateCourseAsync(int id, CourseUpdateDto dto);
    Task<bool> DeleteCourseAsync(int id);
}
