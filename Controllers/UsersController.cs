using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Data;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Services;

namespace Backend_Mobile_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("customerslist")]
        public async Task<IActionResult> GetPagedCustomers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (customers, totalPages) = await _userService.GetCustomers(page, pageSize);
            var result = new
            {
                data = customers.Select(c => new UserDTO
                {
                    UserId = c.UserId,
                    UserName = c.UserName,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                   
                }),
                totalPages
            };
            return Ok(result);
        }

        [HttpGet("shipperslist")]
        public async Task<IActionResult> GetPagedShippers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (shippers, totalPages) = await _userService.GetPagedShippersAsync(page, pageSize);

            var result = new
            {
                data = shippers.Select(s => new ShipperDTO
                {
                    UserId = s.UserId,
                    UserName = s.UserName,
                    PhoneNumber = s.PhoneNumber,
                    Email = s.Email,
                    VehicleType = s.Assignment?.Vehicle?.VehicleType,
                    VehiclePlate = s.Assignment?.Vehicle?.LicensePlate
                }),
                totalPages
            };

            return Ok(result);
        }
        [HttpPost("addshipper")]
        public async Task<IActionResult> addShipper([FromBody] RegisterDTO model)
        {
            var result = await _userService.RegisterAsync(model);
            if (!result) return BadRequest("Email tồn tại");
            return Ok("Them thành công");
        }
        [HttpPut("updateshipper/{id}")]
        public async Task<IActionResult> updateShipper(string id, [FromBody] RegisterDTO model)
        {
            var result = await _userService.UpdateUserAsync(id, model);
            if (!result) return NotFound();
            return Ok("Cập nhật thành công");
        }
        [HttpDelete("deleteshipper/{id}")]
        public async Task<IActionResult> deleteShipper(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();
            return Ok("Xoá thành công");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] RegisterDTO model)
        {
            var result = await _userService.UpdateUserAsync(id, model);
            if (!result) return NotFound();
            return Ok("Cập nhật thành công");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();
            return Ok("Xoá thành công");
        }
        

    }

}