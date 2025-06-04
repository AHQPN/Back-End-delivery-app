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

        //Lay ra tat ca cac orders
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(string id)
        {
            try
            {
                return Ok(await _orderService.GetOrderByOrderIdAsync(id)); 
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
