using System.Collections.Generic;
using System.Threading.Tasks;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Repositories;

namespace Backend_Mobile_App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                
                Latitude = u.Latitude,
                Longitude = u.Longitude
            });
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var u = await _userRepository.GetByIdAsync(id);
            if (u == null) return null;
            return new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                
                Latitude = u.Latitude,
                Longitude = u.Longitude
            };
        }

        public async Task AddAsync(UserDto userDto)
        {
            var user = new User
            {
                UserId = userDto.UserId,
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
               
                Latitude = userDto.Latitude,
                Longitude = userDto.Longitude
            };
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            var user = new User
            {
                UserId = userDto.UserId,
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                
                Latitude = userDto.Latitude,
                Longitude = userDto.Longitude
            };
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
