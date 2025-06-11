using Backend_Mobile_App.Data;
using Backend_Mobile_App.DTOs;
using System.Linq;
using System.Threading.Tasks;

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
            var totalOrders = _context.Orders.Count();
            var totalRevenue = _context.Orders.Sum(o => o.TotalAmount);

            return await Task.FromResult(new OrderStatisticsDto
            {
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue
            });
        }
    }
}