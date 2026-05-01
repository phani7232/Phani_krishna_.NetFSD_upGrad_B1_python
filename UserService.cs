using AutoMapper;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.Entities;
using ELearningAPI.Repositories;

namespace ELearningAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<UserDto> RegisterUserAsync(UserRegisterDto registerDto)
    {
        var existingUser = await _repo.GetUserByEmailAsync(registerDto.Email);
        if (existingUser != null) throw new Exception("Email already in use.");

        var user = _mapper.Map<User>(registerDto);
        
        // Securely hash the password before saving to SQL
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        await _repo.AddUserAsync(user);
        await _repo.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }
    public async Task<UserDto> LoginUserAsync(string email, string plainTextPassword)
    {
    // 1. Find the user in the database by their email
    var user = await _repo.GetUserByEmailAsync(email);
    
    // If the email doesn't exist, stop here
    if (user == null) 
    {
        throw new Exception("Invalid email or password.");
    }

    // 2. THE MAGIC STEP: Verify the password
    // BCrypt automatically extracts the salt from the stored hash and securely compares them
    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(plainTextPassword, user.PasswordHash);

    if (!isPasswordValid) 
    {
        throw new Exception("Invalid email or password.");
    }

    // 3. If everything is correct, map to DTO and return the user!
    return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _repo.GetUserByIdAsync(id);
        if (user == null) return null;
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUserAsync(int id, UserUpdateDto dto)
    {
        var user = await _repo.GetUserByIdAsync(id);
        if (user == null) return null;

        // AutoMapper overwrites the properties from the DTO onto the existing User
        _mapper.Map(dto, user);
        
        await _repo.UpdateUserAsync(user);
        await _repo.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        // Passes the request straight down to the repository
        return await _repo.GetUserByEmailAsync(email);
    }
}