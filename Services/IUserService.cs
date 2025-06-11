using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;

namespace Backend_Mobile_App.Services
{
    public interface IUserService
    {
        Task<LoginResultDTO> LoginAsync(LoginDTO model);
        Task<bool> RegisterAsync(RegisterDTO model);

        Task<UserDTO> VerifyToken(string token);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetByIdAsync(string id);
        Task<bool> UpdateUserAsync(string id, RegisterDTO model);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> UpdateShipperAsync(string id, UserDTO model);
        Task<List<User>> GetPagedShippersAsync();
        Task<List<User>> GetCustomers();


    }

}
