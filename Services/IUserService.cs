using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_Mobile_App.DTOs;

namespace Backend_Mobile_App.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(string id);
        Task AddAsync(UserDTO UserDTO);
        Task UpdateAsync(UserDTO UserDTO);
        Task DeleteAsync(string id);
    }
}
