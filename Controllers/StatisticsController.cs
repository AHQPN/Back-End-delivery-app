using Backend_Mobile_App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrderStatistics()
        {
            var stats = await _statisticsService.GetOrderStatisticsAsync();
            return Ok(stats);
        }
    }
}