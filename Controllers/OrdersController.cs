using Backend_Mobile_App.Data;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Repositories;
using Backend_Mobile_App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Mobile_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly Tracking_ShipmentContext _context;
        private readonly IOrderRepository _orderService;
        public OrdersController(Tracking_ShipmentContext context, IOrderRepository orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        

        //Lưu một order mới vào DB
        [HttpPost("saveOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            if (orderDto == null || orderDto.OrderItems == null || orderDto.Payment == null)
            {
                return BadRequest("Order data is invalid.");
            }
            var newOrderId = await _orderService.AddOrderAsync(orderDto);
            var orderResponse = await _orderService.GetOrderByOrderIdAsync(newOrderId);
            return CreatedAtAction(nameof(GetOrder), new { controller = "Orders", id = newOrderId }, orderResponse);
        }

        //Lay ra order theo id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            try
            {
                return Ok(await _orderService.GetOrderByOrderIdAsync(id)); 
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = "Không thể xử lý yêu cầu", detail = ex.Message });
            }
        }

        //Lay tat ca cac loai hang
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                return Ok(await _orderService.GetAllCategoriesAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Lay tat ca cac sizes
        [HttpGet("sizes")]
        public async Task<IActionResult> GetAllSizes()
        {
            try
            {
                return Ok(await _orderService.GetAllSizesAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //Lay tat ca cac loai dich vu
        [HttpGet("services")]
        public async Task<IActionResult> GetAllServices()
        {
            try
            {
                return Ok(await _orderService.GetAllServicesAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("vehicles")]
        public async Task<IActionResult> GetAllVehicles()
        {
            try
            {
                return Ok(await _orderService.GetAllVehiclesAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("{orderId}/status")]
        public IActionResult UpdateOrderStatus(string orderId, [FromBody] string status)
        {
            var allowedStatuses = new[] { "Đang chờ", "Đang giao", "Đã hoàn tất", "Đã huỷ" };
            if (!allowedStatuses.Contains(status))
            {
                return BadRequest("Invalid status.");
            }

            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderStatus = status;
            _context.SaveChanges();

            return Ok(order);
        }

    }
}
