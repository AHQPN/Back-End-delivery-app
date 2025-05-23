using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Data;

namespace Backend_Mobile_App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TrackingShipmentContext _context;

        public UserRepository(TrackingShipmentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();

        public async Task<User?> GetByIdAsync(string id) => await _context.Users.FindAsync(id);

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

        public async Task DeleteAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
