using Backend_Mobile_App.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Backend_Mobile_App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
       
        private readonly Tracking_ShipmentContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(Tracking_ShipmentContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

       

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Email already registered");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var newUser = new User
            {
                UserId = Guid.NewGuid().ToString("N").Substring(0, 10),
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = hashedPassword,
                Role = model.Role
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok("Đăng kí thành công");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || string.IsNullOrEmpty(user.Password) || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return Unauthorized("Email hoặc password không đúng");

            var token = GenerateJwtToken(user);

            var userDto = new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                Token = token
            };

            return Ok(userDto);
        }

        
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinutes = int.Parse(jwtSettings["ExpiryMinutes"]);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
