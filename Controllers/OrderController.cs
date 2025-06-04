using Backend_Mobile_App.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Mobile_App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {        
        private readonly Tracking_ShipmentContext _context;
        private readonly IConfiguration _configuration;

        [HttpPost("order")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
