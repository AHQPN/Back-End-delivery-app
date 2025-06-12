using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Data;

namespace Backend_Mobile_App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Tracking_ShipmentContext _context;

        public UserRepository(Tracking_ShipmentContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        public async Task<int> CreateEmptyLocationAsync()
        {
            var location = new Location
            {
                Latitude = null,
                Longitude = null
            };

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
            return location.LocationId;
        }

        public async Task<List<User>> GetShippersAsync()
        {
            return await _context.Users
                .Where(u => u.Role == "Shipper")
                .Include(u => u.Assignment)           
                    .ThenInclude(a => a.Vehicle)      
                .ToListAsync();
        }

        public async Task<List<User>> GetCustomerAsync()
        {
            return await _context.Users
                .Where(u => u.Role == "Customer")
                .ToListAsync();
        }
        
    }

}
