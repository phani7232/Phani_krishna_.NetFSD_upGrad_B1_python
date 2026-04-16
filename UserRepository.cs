using ELearningAPI.Data;
using ELearningAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context) => _context = context;

    public async Task<User> GetUserByEmailAsync(string email) => 
        await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

    public async Task AddUserAsync(User user) => await _context.Users.AddAsync(user);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    
    public async Task<User> GetUserByIdAsync(int id) => 
        await _context.Users.FindAsync(id);

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
    }
}