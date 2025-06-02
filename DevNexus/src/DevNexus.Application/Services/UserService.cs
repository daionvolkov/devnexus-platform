using DevNexus.Core.Entities;
using DevNexus.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DevNexus.Application.Services;

public class UserService(DevNexusDbContext context)
{
    public async Task<IEnumerable<User>> GetAllLessonsAsync() => await context.Users.ToListAsync();

    public async Task<User?> GetByIdAsync(int id) => await context.Users.FindAsync(id);

    
    public async Task<User> CreateAsync(User user)
    {
        user.RegisteredAt = DateTime.UtcNow;
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }


    public async Task<bool> UpdateAsync(User user)
    {
        var existing = await context.Users.FindAsync(user.Id);
        if (existing == null) return false;
        existing.FullName = user.FullName;
           
        await context.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null) return false;
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return true;
    }

}