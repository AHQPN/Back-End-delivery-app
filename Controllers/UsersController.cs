using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Data;
using Backend_Mobile_App.Repositories;

namespace Backend_Mobile_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Tracking_ShipmentContext _context; // Sử dụng Tracking_ShipmentContext của bạn
        private readonly ILocationRepository locationService;

        public UsersController(Tracking_ShipmentContext context, ILocationRepository _locationService) // Inject Tracking_ShipmentContext
        {
            _context = context;
            locationService = _locationService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync(); // Truy vấn tất cả người dùng
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id); // Tìm người dùng theo UserId

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'Tracking_ShipmentContext.Users' is null.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        // GET: api/Users/{id}/location
        [HttpGet("{id}/location")]
        public async Task<ActionResult<Location>> GetUserLocation(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.UserLocationNavigation)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null || user.UserLocationNavigation == null)
            {
                return NotFound();
            }
            var location = user.UserLocationNavigation;
            return Ok(new
            {
                location.LocationId,
                location.Latitude,
                location.Longitude
            });
        }

        [HttpPut("{id}/location")]
        public async Task<IActionResult> SaveOrUpdateUserLocation(string id, [FromBody] Location locationDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var location = await locationService.SaveLocationByLatLongAsync(locationDto.Latitude ?? 0, locationDto.Longitude ?? 0);

            if (location == null)
            {
                return BadRequest("Could not save location.");
            }

            user.UserLocation = location.LocationId;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
