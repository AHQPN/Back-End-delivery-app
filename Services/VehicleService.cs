using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Services
{
    public class VehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public Task<string> AddVehicleAsync(VehicleDTO vehicleDto)
        {
            return _vehicleRepository.AddVehicleAsync(vehicleDto);
        }

        public Task<List<VehicleDTO>> GetAllVehiclesAsync()
        {
            return _vehicleRepository.GetAllVehiclesAsync();
        }

        public Task<VehicleDTO?> GetVehicleByIdAsync(string vehicleId)
        {
            return _vehicleRepository.GetVehicleByIdAsync(vehicleId);
        }

        public Task<bool> UpdateVehicleAsync(string vehicleId, VehicleDTO vehicleDto)
        {
            return _vehicleRepository.UpdateVehicleAsync(vehicleId, vehicleDto);
        }

        public Task<bool> DeleteVehicleAsync(string vehicleId)
        {
            return _vehicleRepository.DeleteVehicleAsync(vehicleId);
        }
    }
}
