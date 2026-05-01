using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.Models.DTOs;

public class CourseDto
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<LessonDto> Lessons { get; set; } = new();
}

public class CourseCreateDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; }
    public string Description { get; set; }
    public int CreatedBy { get; set; }
}

public class CourseUpdateDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; }
    public string Description { get; set; }
}