using AutoMapper;
using ELearningAPI.Data;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Controllers;

[ApiController]
[Route("api")]
public class LessonsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LessonsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("courses/{courseId}/lessons")]
    public async Task<IActionResult> GetLessonsForCourse(int courseId)
    {
        var lessons = await _context.Lessons
            .Where(l => l.CourseId == courseId)
            .AsNoTracking()
            .ToListAsync();
            
        return Ok(_mapper.Map<IEnumerable<LessonDto>>(lessons));
    }

    [HttpPost("lessons")]
    public async Task<IActionResult> CreateLesson([FromBody] LessonCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var lesson = _mapper.Map<Lesson>(dto);
        await _context.Lessons.AddAsync(lesson);
        await _context.SaveChangesAsync();

        return Created($"/api/lessons/{lesson.LessonId}", _mapper.Map<LessonDto>(lesson));
    }

    [HttpPut("lessons/{id}")]
    public async Task<IActionResult> UpdateLesson(int id, [FromBody] LessonUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null) return NotFound();

        _mapper.Map(dto, lesson);
        _context.Lessons.Update(lesson);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<LessonDto>(lesson));
    }

    [HttpDelete("lessons/{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null) return NotFound();

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}