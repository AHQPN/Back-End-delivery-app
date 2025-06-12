using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleService _vehicleService;

        public VehiclesController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleDTO>>> GetAllVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDTO>> GetVehicleById(string id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
                return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddVehicle([FromBody] VehicleDTO vehicleDto)
        {
            var vehicleId = await _vehicleService.AddVehicleAsync(vehicleDto);
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicleId }, new { id = vehicleId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(string id, [FromBody] VehicleDTO vehicleDto)
        {
            var updated = await _vehicleService.UpdateVehicleAsync(id, vehicleDto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(string id)
        {
            var deleted = await _vehicleService.DeleteVehicleAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
