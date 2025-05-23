using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_Mobile_App.Models;

namespace Backend_Mobile_App.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}
