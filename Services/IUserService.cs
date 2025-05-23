using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_Mobile_App.DTOs;

namespace Backend_Mobile_App.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(string id);
        Task AddAsync(UserDto userDto);
        Task UpdateAsync(UserDto userDto);
        Task DeleteAsync(string id);
    }
}
