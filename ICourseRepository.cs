using ELearningAPI.Models.Entities;

namespace ELearningAPI.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<Course> GetCourseByIdAsync(int id);
    Task AddCourseAsync(Course course);
    Task SaveChangesAsync();
    Task UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(Course course);

}