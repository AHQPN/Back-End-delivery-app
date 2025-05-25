//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Backend_Mobile_App.DTOs;
//using Backend_Mobile_App.Models;
//using Backend_Mobile_App.Repositories;

//namespace Backend_Mobile_App.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly IUserRepository _userRepository;

//        public UserService(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public async Task<IEnumerable<UserDTO>> GetAllAsync()
//        {
//            var users = await _userRepository.GetAllAsync();
//            return users.Select(u => new UserDTO
//            {
//                UserId = u.UserId,
//                UserName = u.UserName,
//                Email = u.Email,
//                PhoneNumber = u.PhoneNumber,
                
//                Latitude = u.Latitude,
//                Longitude = u.Longitude
//            });
//        }

//        public async Task<UserDTO?> GetByIdAsync(string id)
//        {
//            var u = await _userRepository.GetByIdAsync(id);
//            if (u == null) return null;
//            return new UserDTO
//            {
//                UserId = u.UserId,
//                UserName = u.UserName,
//                Email = u.Email,
//                PhoneNumber = u.PhoneNumber,
                
//                Latitude = u.Latitude,
//                Longitude = u.Longitude
//            };
//        }

//        public async Task AddAsync(UserDTO UserDTO)
//        {
//            var user = new User
//            {
//                UserId = UserDTO.UserId,
//                UserName = UserDTO.UserName,
//                Email = UserDTO.Email,
//                PhoneNumber = UserDTO.PhoneNumber,
               
//                Latitude = UserDTO.Latitude,
//                Longitude = UserDTO.Longitude
//            };
//            await _userRepository.AddAsync(user);
//        }

//        public async Task UpdateAsync(UserDTO UserDTO)
//        {
//            var user = new User
//            {
//                UserId = UserDTO.UserId,
//                UserName = UserDTO.UserName,
//                Email = UserDTO.Email,
//                PhoneNumber = UserDTO.PhoneNumber,
                
//                Latitude = UserDTO.Latitude,
//                Longitude = UserDTO.Longitude
//            };
//            await _userRepository.UpdateAsync(user);
//        }

//        public async Task DeleteAsync(string id)
//        {
//            await _userRepository.DeleteAsync(id);
//        }
//    }
//}
