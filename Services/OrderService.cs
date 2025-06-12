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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (orderCreateDto == null)
                {
                    throw new ArgumentNullException(nameof(orderCreateDto), "Dữ liệu tạo đơn hàng không được để trống.");
                }

                // Kiểm tra CustomerID tồn tại
                if (string.IsNullOrEmpty(orderCreateDto.CustomerId) || await _context.Users.FindAsync(orderCreateDto.CustomerId) == null)
                {
                    throw new Exception("CustomerId không hợp lệ hoặc không tồn tại.");
                }

                // Kiểm tra ServiceID (nếu có)
                if (!string.IsNullOrEmpty(orderCreateDto.Serviceid) && await _context.Services.FindAsync(orderCreateDto.Serviceid) == null)
                {
                    throw new Exception("Serviceid không hợp lệ hoặc không tồn tại.");
                }

                //Thêm vào location mới nếu chưa tồn tại
                //Xử lý source location
                int? sourceLocationId = null;
                if (orderCreateDto.SourceLocation != null && orderCreateDto.SourceLocation.Latitude.HasValue && orderCreateDto.SourceLocation.Longitude.HasValue)
                {
                    var exsistingSourceLocation = await _context.Locations.FirstOrDefaultAsync(
                        l => l.Latitude.HasValue && l.Longitude.HasValue &&
                        orderCreateDto.SourceLocation != null &&
                        Math.Abs(l.Latitude.Value - orderCreateDto.SourceLocation.Latitude.Value) <= 0.000001m &&
                        Math.Abs(l.Longitude.Value - orderCreateDto.SourceLocation.Longitude.Value) <= 0.000001m);

                    if (exsistingSourceLocation != null)
                    {
                        sourceLocationId = exsistingSourceLocation.LocationId;
                    }
                    else
                    {
                        var newSourceLocationDTO = new LocationDTO(
                            Math.Round(orderCreateDto.SourceLocation.Latitude.Value, 6),
                            Math.Round(orderCreateDto.SourceLocation.Longitude.Value, 6)
                            );

                        var newSourceLocation = _mapper.Map<Location>(newSourceLocationDTO);
                        _context.Locations!.Add(newSourceLocation);
                        await _context.SaveChangesAsync();
                        sourceLocationId = newSourceLocation.LocationId;
                    }
                }

                //Xử lý destination location
                int? destinationLocationId = null;
                if (orderCreateDto.DestinationLocation != null && orderCreateDto.DestinationLocation.Latitude.HasValue && orderCreateDto.DestinationLocation.Longitude.HasValue)
                {
                    var exsistingDestinationLocation = await _context.Locations.FirstOrDefaultAsync(
                        l => l.Latitude.HasValue && l.Longitude.HasValue &&
                        orderCreateDto.DestinationLocation != null &&
                        Math.Abs(l.Latitude.Value - orderCreateDto.DestinationLocation.Latitude.Value) <= 0.000001m &&
                        Math.Abs(l.Longitude.Value - orderCreateDto.DestinationLocation.Longitude.Value) <= 0.000001m);

                    if (exsistingDestinationLocation != null)
                    {
                        destinationLocationId = exsistingDestinationLocation.LocationId;
                    }
                    else
                    {
                        var newDestinationLocationDTO = new LocationDTO(
                             Math.Round(orderCreateDto.DestinationLocation.Latitude.Value, 6),
                             Math.Round(orderCreateDto.DestinationLocation.Longitude.Value, 6)
                            );

                        var newDestinationLocation = _mapper.Map<Location>(newDestinationLocationDTO);
                        _context.Locations!.Add(newDestinationLocation);
                        await _context.SaveChangesAsync();
                        destinationLocationId = newDestinationLocation.LocationId;
                    }
                }

                //Tạo mới một order
                var newOrder = _mapper.Map<Order>(orderCreateDto);
                string orderId = "O" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
                newOrder.OrderId = orderId;
                newOrder.CreatedAt = DateTime.Now;
                newOrder.OrderStatus = "Đang chờ";
                newOrder.SourceLocation = sourceLocationId;
                newOrder.DestinationLocation = destinationLocationId;

                newOrder.CustomerId = orderCreateDto.CustomerId;
                newOrder.Serviceid = orderCreateDto.Serviceid;
                newOrder.DeliveryPersonId = orderCreateDto.DeliveryPersonId;
                newOrder.EstimatedDeliveryTime = orderCreateDto.EstimatedDeliveryTime;
                newOrder.ActualDeliveryTime = orderCreateDto.ActualDeliveryTime;
                newOrder.PickupTime = orderCreateDto.PickupTime;
                newOrder.TenNguoiNhan = orderCreateDto.TenNguoiNhan;
                newOrder.SdtnguoiNhan = orderCreateDto.SdtnguoiNhan;
                newOrder.TotalAmount = orderCreateDto.TotalAmount;

                // Thêm OrderItems
                if (orderCreateDto.CustomerId != null)
                {
                    newOrder.OrderItems = _mapper.Map<List<OrderItem>>(orderCreateDto.OrderItems);
                    foreach (var item in newOrder.OrderItems)
                    {
                        item.OrderItemId = "OI" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
                        item.OrderId = orderId;

                        // Kiểm tra SizeID và CategoryID
                        if (!string.IsNullOrEmpty(item.SizeId) && await _context.Sizes.FindAsync(item.SizeId) == null)
                        {
                            throw new Exception($"SizeID {item.SizeId} không tồn tại.");
                        }
                        if (!string.IsNullOrEmpty(item.CategoryId) && await _context.Categories.FindAsync(item.CategoryId) == null)
                        {
                            throw new Exception($"CategoryID {item.CategoryId} không tồn tại.");
                        }
                    }
                }


                // Thêm Payment
                if (orderCreateDto.Payment != null)
                {
                    var newPaymment = _mapper.Map<Payment>(orderCreateDto.Payment);
                    newPaymment.PaymentId = "PMT" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 7);
                    newPaymment.OrderId = orderId;
                    newPaymment.CreatedAt = DateTime.Now;

                    var validPaymentStatuses = new[] { "Thành công", "Thất bại", "Chờ xử lý" };
                    if (string.IsNullOrEmpty(newPaymment.PaymentStatus) || !validPaymentStatuses.Contains(newPaymment.PaymentStatus))
                    {
                        throw new Exception("PaymentStatus không hợp lệ.");
                    }

                    newOrder.Payment = newPaymment;
                }
                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return orderId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }


        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public async Task<List<OrderCreateDto>> GetAllOders()
        {
            var orders = await _context.Orders!.ToListAsync();
            return _mapper.Map<List<OrderCreateDto>>(orders);
        }

        public async Task<List<OrderResponseDTO>> GetAllOdersByCustomerIdAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                throw new ArgumentNullException(nameof(customerId), "customerId không được để trống.");
            }

            var ordersDTO = await _context.Orders
               .AsNoTracking()
               .Where(o => o.CustomerId == customerId)
               .Select(o => new OrderResponseDTO
               {
                   OrderID = o.OrderId,

                   SourceLocation = o.SourceLocationNavigation != null ? new LocationDTO
                   (
                       o.SourceLocationNavigation.Latitude,
                       o.SourceLocationNavigation.Longitude
                   ) : null,

                   DestinationLocation = o.DestinationLocationNavigation != null ? new LocationDTO
                   (
                       o.DestinationLocationNavigation.Latitude,
                       o.DestinationLocationNavigation.Longitude
                   ) : null,

                   VehicleId = o.VehicleId,
                   TotalAmount = o.TotalAmount,
                   OrderStatus = o.OrderStatus,
                   PaymentStatus = o.Payment != null ? o.Payment.PaymentStatus : null,
                   CreatedAt = o.CreatedAt,

                   CustomerId = o.CustomerId,
                   TenNguoiNhan = o.TenNguoiNhan

               }).ToListAsync();

            if (ordersDTO == null || !ordersDTO.Any())
            {
                throw new Exception($"Không tìm thấy đơn hàng với CustomerId: {customerId}.");
            }

            return ordersDTO;
        }

        public async Task<List<OrderResponseDTO>> GetAllOdersByShipperIdAsync(string shipperId)
        {

            if (string.IsNullOrEmpty(shipperId))
            {
                throw new ArgumentNullException(nameof(shipperId), "shipperId không được để trống.");
            }

            var ordersDTO = await _context.Orders
               .AsNoTracking()
               .Where(o => o.DeliveryPersonId == shipperId && (o.OrderStatus == "Đang giao" || o.OrderStatus == "Đang chờ"))
               .Select(o => new OrderResponseDTO
               {
                   OrderID = o.OrderId,

                   SourceLocation = o.SourceLocationNavigation != null ? new LocationDTO
                   (
                       o.SourceLocationNavigation.Latitude,
                       o.SourceLocationNavigation.Longitude
                   ) : null,

                   DestinationLocation = o.DestinationLocationNavigation != null ? new LocationDTO
                   (
                       o.DestinationLocationNavigation.Latitude,
                       o.DestinationLocationNavigation.Longitude
                   ) : null,

                   VehicleId = o.VehicleId,
                   TotalAmount = o.TotalAmount,
                   OrderStatus = o.OrderStatus,
                   PaymentStatus = o.Payment != null ? o.Payment.PaymentStatus : null,
                   CreatedAt = o.CreatedAt,

                   CustomerId = o.CustomerId,
                   TenNguoiNhan = o.TenNguoiNhan

               }).ToListAsync();

            if (ordersDTO == null || !ordersDTO.Any())
            {
                throw new Exception($"Không tìm thấy đơn hàng với CustomerId: {shipperId}.");
            }

            return ordersDTO;
        }

        public async Task<List<ServiceDTO>> GetAllServicesAsync()
        {
            var services = await _context.Services.ToListAsync();
            return _mapper.Map<List<ServiceDTO>>(services);
        }

        public async Task<List<SizeDTO>> GetAllSizesAsync()
        {
            var sizes = await _context.Sizes.ToListAsync();
            return _mapper.Map<List<SizeDTO>>(sizes);
        }

        public async Task<List<VehicleDTO>> GetAllVehiclesAsync()
        {
            var vehicles = await _context.Vehicles.ToListAsync();
            return _mapper.Map<List<VehicleDTO>>(vehicles);
        }

        public async Task<OrderResponseDTO> GetOrderByOrderIdAsync(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentNullException(nameof(orderId), "OrderID không được để trống.");
            }

            var order = await _context.Orders
                .Include(o => o.SourceLocationNavigation)
                .Include(o => o.DestinationLocationNavigation)
                .Include(o => o.Payment)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                throw new Exception($"Không tìm thấy đơn hàng với OrderID: {orderId}.");
            }

            var orderDTO = _mapper.Map<OrderResponseDTO>(order);

            orderDTO.SourceLocation = order.SourceLocationNavigation != null
                ? new LocationDTO(order.SourceLocationNavigation.Latitude, order.SourceLocationNavigation.Longitude)
                : null;

            orderDTO.DestinationLocation = order.DestinationLocationNavigation != null
                ? new LocationDTO(order.DestinationLocationNavigation.Latitude, order.DestinationLocationNavigation.Longitude)
                : null;

            return orderDTO;
        }
    }
}




