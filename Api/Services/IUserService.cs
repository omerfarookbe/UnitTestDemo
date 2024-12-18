using Api.Models;

namespace Api.Services
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);

        Task<User> UpdateAsync(User user);

        Task<bool> DeleteAsync(int id);

        Task<List<User>> GetAllAsync();

        Task<User> GetByIdAsync(int id);
    }
}
