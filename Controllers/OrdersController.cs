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
        //public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderDto)
        //{
        //    if (orderDto == null || orderDto.OrderItems == null || orderDto.Payment == null)
        //    {
        //        return BadRequest("Order data is invalid.");
        //    }

            

        //    //return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        //}

        //Lay ra tat ca cac orders
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                return Ok(await _orderService.GetAllOders()); 
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
