using AutoMapper;
using Backend_Mobile_App.Controllers;
using Backend_Mobile_App.Data;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Mobile_App.Services
{
    public class OrderService : IOrderRepository
    {
        public readonly Tracking_ShipmentContext _context;
        public readonly IMapper _mapper;

        public OrderService(Tracking_ShipmentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> AddOrderAsync(OrderCreateDto orderCreateDto)
        {
            throw new NotImplementedException();
            if (orderCreateDto == null)
            {
                throw new ArgumentNullException(nameof(orderCreateDto), "Dữ liệu tạo đơn hàng không được để trống.");
            }

            // Kiểm tra CustomerID tồn tại
            if (string.IsNullOrEmpty(orderCreateDto.CustomerID) || await _context.Users.FindAsync(orderCreateDto.CustomerID) == null)
            {
                throw new Exception("CustomerID không hợp lệ hoặc không tồn tại.");
            }

            //Tạo mới một order
            var newOrder = _mapper.Map<Order>(orderCreateDto);
            string orderId = "O" + Guid.NewGuid().ToString();
            newOrder.OrderId = orderId;
            newOrder.CreatedAt = DateTime.Now;
            newOrder.OrderStatus = "Đang chờ";

            // Thêm OrderItems
            if (orderCreateDto.CustomerID != null)
            {
                 newOrder.OrderItems = _mapper.Map<List<OrderItem>>(orderCreateDto.OrderItems);
                foreach (var item in newOrder.OrderItems)
                {
                    item.OrderItemId = "OI" + Guid.NewGuid().ToString();
                    item.OrderId = orderId;
                }
            }
            

            // Thêm Payment
            //var payment = new Payment
            //{
            //    PaymentId = "PMT" + Guid.NewGuid().ToString(),
            //    OrderId = order.OrderId,
            //    PaymentMethod = orderDto.Payment.PaymentMethod,
            //    PaymentStatus = orderDto.Payment.PaymentStatus,
            //    TransactionId = orderDto.Payment.TransactionID,
            //    Amount = orderDto.TotalAmount,
            //    CreatedAt = DateTime.UtcNow
            //};
            //order.Payments.Add(payment);

            //_context.Orders.Add(order);
            //await _context.SaveChangesAsync();

            //payment.OrderId = order.OrderId;
            //await _context.SaveChangesAsync();
            //_context.Orders!.Add(newOder);
            //await _context.SaveChangesAsync();

            //return newOder.OrderId;
        }

        public async Task<List<OrderCreateDto>> GetAllOders()
        {
            var orders = await _context.Orders!.ToListAsync();
            return _mapper.Map<List<OrderCreateDto>>(orders);
        }

        Task<List<OrderResponseDTO>> IOrderRepository.GetAllOdersByCustomerId(string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
