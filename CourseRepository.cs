using ELearningAPI.Data;
using ELearningAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        // Requirements Met: AsNoTracking for read performance, Include for eager loading
        return await _context.Courses
            .Include(c => c.Lessons)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Lessons)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CourseId == id);
    }

    public async Task AddCourseAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCourseAsync(Course course)
    {
        _context.Courses.Update(course);
    }

    public async Task DeleteCourseAsync(Course course)
    {
        _context.Courses.Remove(course);
    }
}