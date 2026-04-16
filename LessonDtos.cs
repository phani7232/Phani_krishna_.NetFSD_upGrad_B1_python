using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.Models.DTOs;

public class LessonDto
{
    public int LessonId { get; set; }
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int OrderIndex { get; set; }
}

public class LessonCreateDto
{
    [Required] public int CourseId { get; set; }
    [Required, MaxLength(200)] public string Title { get; set; }
    [Required] public string Content { get; set; }
    public int OrderIndex { get; set; }
}
public class LessonUpdateDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    public int OrderIndex { get; set; }
}