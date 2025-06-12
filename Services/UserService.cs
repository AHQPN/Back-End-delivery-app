using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Repositories;
using Backend_Mobile_App.Services; // Ensure IUserService is here
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using AutoMapper; // Add this using directive for IConfiguration

// This is the correct and only definition of your UserService class
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    public readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper; 
        _userRepository = userRepository;
        _configuration = configuration;
    }
    public async Task<bool> ChangePasswordAsync(string userId, string newPassword)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error changing password: " + ex.Message);
            return false;
        }
    }
    public async Task<bool> RegisterAsync(RegisterDTO model)
    {
        var existingUser = await _userRepository.GetByEmailAsync(model.Email);
        if (existingUser != null)
            return false;

        int locationId = await _userRepository.CreateEmptyLocationAsync();

        var newUser = new User
        {
            UserId = Guid.NewGuid().ToString("N").Substring(0, 10),
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Role = model.Role,
            UserLocation = locationId
        };

        await _userRepository.AddAsync(newUser);
        return true;
    }

    public async Task<LoginResultDTO> LoginAsync(LoginDTO model)
    {
        var user = await _userRepository.GetByEmailAsync(model.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            return null;

        var token = GenerateJwtToken(user);
        return new LoginResultDTO
        {
            Token = token,
            User = new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            }
        };
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDTO
        {
            UserId = u.UserId,
            UserName = u.UserName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            Role = u.Role
        });
    }

    public async Task<UserDTO> GetByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return null;

        return new UserDTO
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role
        };
    }

    public async Task<bool> UpdateUserAsync(string id, RegisterDTO model)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        user.UserName = model.UserName;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        user.Role = model.Role;
        if (!string.IsNullOrEmpty(model.Password))
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

        await _userRepository.UpdateAsync(user);
        return true;
    }
    public async Task<bool> UpdateShipperAsync(string id, UserDTO model)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        user.UserName = model.UserName;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        user.Role = model.Role;
        

        await _userRepository.UpdateAsync(user);
        return true;
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        await _userRepository.DeleteAsync(user);
        return true;
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings["ExpiryMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<List<User>> GetPagedShippersAsync()
    {
        var allShippers = await _userRepository.GetShippersAsync();


        return allShippers;
    }
    public async Task<List<User>> GetCustomers()
    {
        var allCustomer = await _userRepository.GetCustomerAsync();

        return allCustomer;
    }
    public async Task<UserDTO> VerifyToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenHandler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            Console.WriteLine(principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value);
            
            var userId = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            User user = await _userRepository.GetByIdAsync(userId);
            UserDTO newdto = _mapper.Map<UserDTO>(user);
            return newdto;
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Token validation failed: " + ex.Message);
            return null;
        }

    }
    public async Task<bool> AddShipper(RegisterDTO model)
    {
        var existingUser = await _userRepository.GetByEmailAsync(model.Email);
        if (existingUser != null)
            return false;

        int locationId = await _userRepository.CreateEmptyLocationAsync();

        var newUser = new User
        {
            UserId = Guid.NewGuid().ToString("N").Substring(0, 10),
            UserName = model.UserName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Role = model.Role,
            UserLocation = locationId
        };

        await _userRepository.AddAsync(newUser);
        return true;
    }
}