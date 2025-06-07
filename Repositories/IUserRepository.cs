using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_Mobile_App.Models;

namespace Backend_Mobile_App.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(string id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<int> CreateEmptyLocationAsync();

    }

}
