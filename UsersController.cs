using ELearningAPI.Models.DTOs;
using ELearningAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELearningAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        // Requirement Met: Validate all inputs
        if (!ModelState.IsValid) return BadRequest(ModelState); // 400

        try
        {
            var userDto = await _userService.RegisterUserAsync(dto);
            return CreatedAtAction(nameof(Register), new { id = userDto.UserId }, userDto); // 201
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message }); // 400
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var updatedUser = await _userService.UpdateUserAsync(id, dto);
        if (updatedUser == null) return NotFound();
        
        return Ok(updatedUser);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] ELearningAPI.Models.DTOs.UserRegisterDto loginDto)
    {
    // Note: We are reusing UserRegisterDto here just to capture Email and Password easily
    var user = await _userService.GetUserByEmailAsync(loginDto.Email); // We need to add this to UserService!
    
    if (user == null) 
    {
        return Unauthorized("User must register first.");
    }

    if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
    {
        return Unauthorized("Incorrect password.");
    }

    return Ok(new { user.UserId, user.FullName, user.Email });
   }
}