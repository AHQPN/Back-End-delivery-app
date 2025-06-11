using Backend_Mobile_App.DTOs;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Services
{
    public interface IStatisticsService
    {
        Task<OrderStatisticsDto> GetOrderStatisticsAsync();
    }
}