using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _service;
        public AssignmentController(IAssignmentService service)
        {
            _service = service;
        }

        [HttpPost("assign-order")]
        public async Task<IActionResult> AssignOrder([FromBody] AssignOrderRequestDto request)
        {
            var result = await _service.AssignOrderToNearestShipperAsync(request.OrderId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}