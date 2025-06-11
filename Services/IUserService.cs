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

        Task<(List<User> Data, int TotalPages)> GetPagedShippersAsync(int page, int pageSize);
        Task<(List<User> Data, int TotalPages)> GetCustomers(int page, int pageSize);


    }

}
