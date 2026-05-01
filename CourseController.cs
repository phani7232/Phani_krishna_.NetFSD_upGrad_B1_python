using ELearningAPI.Models.DTOs;
using ELearningAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELearningAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _courseService.GetAllCoursesAsync();
        return Ok(courses); // 200 OK
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CourseCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var course = await _courseService.CreateCourseAsync(dto);
        return Created($"/api/courses/{course.CourseId}", course); // 201 Created
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null) return NotFound();
        return Ok(course);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updatedCourse = await _courseService.UpdateCourseAsync(id, dto);
        if (updatedCourse == null) return NotFound();

        return Ok(updatedCourse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var success = await _courseService.DeleteCourseAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}