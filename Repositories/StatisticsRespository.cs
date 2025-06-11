using Backend_Mobile_App.Data;
using Backend_Mobile_App.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Backend_Mobile_App.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly Tracking_ShipmentContext _context;
        public StatisticsRepository(Tracking_ShipmentContext context)
        {
            _context = context;
        }

        public async Task<OrderStatisticsDto> GetOrderStatisticsAsync()
        {
            var tongDonHang = await _context.Orders.CountAsync();
            var tongShipper = await _context.Users.CountAsync(u => u.Role == "Shipper");
            var tongKhach = await _context.Users.CountAsync(u => u.Role == "Customer");
            var tongPhuongTien = await _context.Vehicles.CountAsync();

            // Lấy doanh thu theo từng tháng đã có dữ liệu ActualDeliveryTime
            var doanhThuTheoThangRaw = await _context.Orders
                .Where(o => o.ActualDeliveryTime != null)
                .GroupBy(o => new { o.ActualDeliveryTime.Value.Year, o.ActualDeliveryTime.Value.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    DoanhThu = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync();

            var dictDoanhThu = doanhThuTheoThangRaw
                .ToDictionary(
                    x => $"{x.Month:00}/{x.Year}",
                    x => x.DoanhThu
                );

            return new OrderStatisticsDto
            {
                TongDonHang = tongDonHang,
                TongShipper = tongShipper,
                TongKhach = tongKhach,
                TongPhuongTien = tongPhuongTien,
                DoanhThuTheoThang = dictDoanhThu
            };
        }
    }
}