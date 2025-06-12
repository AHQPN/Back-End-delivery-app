using Backend_Mobile_App.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Repositories
{
    public interface IVehicleRepository
    {
        Task<string> AddVehicleAsync(VehicleDTO vehicleDto);
        Task<List<VehicleDTO>> GetAllVehiclesAsync();
        Task<VehicleDTO?> GetVehicleByIdAsync(string vehicleId);
        Task<bool> UpdateVehicleAsync(string vehicleId, VehicleDTO vehicleDto);
        Task<bool> DeleteVehicleAsync(string vehicleId);
    }
}
