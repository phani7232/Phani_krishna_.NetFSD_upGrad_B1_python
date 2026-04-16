using AutoMapper;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.Entities;
using ELearningAPI.Repositories;

namespace ELearningAPI.Services;

public interface IUserService
{
    Task<UserDto> RegisterUserAsync(UserRegisterDto registerDto);
    Task<UserDto> GetUserByIdAsync(int id);
    Task<UserDto> UpdateUserAsync(int id, UserUpdateDto dto);
    Task<User> GetUserByEmailAsync(string email);
}
