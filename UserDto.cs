using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.Models.DTOs;

public class UserDto
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    // Notice: NO PasswordHash here! Secure!
}

public class UserRegisterDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; }
    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; }
    [Required, MinLength(6)]
    public string Password { get; set; }
}
public class UserUpdateDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; }
    
    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; }
}