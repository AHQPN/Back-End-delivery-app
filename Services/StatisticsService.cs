using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Repositories;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _repo;
        public StatisticsService(IStatisticsRepository repo)
        {
            _repo = repo;
        }

        public async Task<OrderStatisticsDto> GetOrderStatisticsAsync()
        {
            return await _repo.GetOrderStatisticsAsync();
        }
    }
}