using ELearningAPI.Data;
using ELearningAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task SaveChangesAsync();
    Task<User> GetUserByIdAsync(int id);
    Task UpdateUserAsync(User user);
    
}
