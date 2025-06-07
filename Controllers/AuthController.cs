using Backend_Mobile_App.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Backend_Mobile_App.Services;

namespace Backend_Mobile_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _userService.RegisterAsync(model);
            if (!result) return BadRequest("Email tồn tại");
            return Ok("Đăng ký thành công");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _userService.LoginAsync(model);
            if (result == null)
                return Unauthorized("Email hoặc mật khẩu không đúng");

            return Ok(result);
        }
    }

}
