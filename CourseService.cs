using AutoMapper;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.Entities;
using ELearningAPI.Repositories;

namespace ELearningAPI.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repo;
    private readonly IMapper _mapper;

    public CourseService(ICourseRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _repo.GetAllCoursesAsync();
        return _mapper.Map<IEnumerable<CourseDto>>(courses);
    }

    public async Task<CourseDto> CreateCourseAsync(CourseCreateDto dto)
    {
        var course = _mapper.Map<Course>(dto);
        await _repo.AddCourseAsync(course);
        await _repo.SaveChangesAsync();
        return _mapper.Map<CourseDto>(course);
    }
    public async Task<CourseDto> GetCourseByIdAsync(int id)
    {
        var course = await _repo.GetCourseByIdAsync(id);
        return _mapper.Map<CourseDto>(course);
    }

    public async Task<CourseDto> UpdateCourseAsync(int id, CourseUpdateDto dto)
    {
        var course = await _repo.GetCourseByIdAsync(id);
        if (course == null) return null;

        _mapper.Map(dto, course);
        await _repo.UpdateCourseAsync(course);
        await _repo.SaveChangesAsync();

        return _mapper.Map<CourseDto>(course);
    }

    public async Task<bool> DeleteCourseAsync(int id)
    {
        var course = await _repo.GetCourseByIdAsync(id);
        if (course == null) return false;

        await _repo.DeleteCourseAsync(course);
        await _repo.SaveChangesAsync();
        return true;
    }
}