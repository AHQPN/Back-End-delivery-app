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
        public async Task<IActionResult> GetPagedCustomers()
        {
            var result = await _userService.GetCustomers();
            if (result == null || !result.Any())
            {
                return NotFound("Không có khách hàng nào");
            }
            return Ok(result);
        }

        [HttpGet("shipperslist")]
        public async Task<IActionResult> GetPagedShippers()
        {
            var shippers = await _userService.GetPagedShippersAsync();
            if (shippers == null || !shippers.Any())
            {
                return NotFound("Không có shipper nào");
            }
            return Ok(shippers);
        }
        [HttpPost("addshipper")]
        public async Task<IActionResult> addShipper([FromBody] RegisterDTO model)
        {
            var result = await _userService.RegisterAsync(model);
            if (!result) return BadRequest("Email tồn tại");
            return Ok("Them thành công");
        }
        [HttpPut("updateshipper/{id}")]
        
        public async Task<IActionResult> updateShipper(string id, [FromBody] UserDTO model)
        {
            var result = await _userService.UpdateShipperAsync(id, model);
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