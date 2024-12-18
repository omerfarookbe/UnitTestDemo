using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            _dbContext!.Users.Add(user);
            var result = await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var found = await _dbContext.Users.FirstOrDefaultAsync(f => f.Id == user.Id);
            if (found != null)
            {
                found.Name = user.Name;
                found.Email = user.Email;
                var result = await _dbContext.SaveChangesAsync();
                return found;
            }
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var found = await _dbContext.Users.FirstOrDefaultAsync(f => f.Id == id);
            if (found != null)
            {
                _dbContext.Remove(found);
                var result = await _dbContext.SaveChangesAsync() ;
                return result > 0;
            }
            return false;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext!.Users!.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}