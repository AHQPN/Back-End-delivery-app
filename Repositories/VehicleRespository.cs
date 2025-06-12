using Backend_Mobile_App.Data;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly Tracking_ShipmentContext _context;

        public VehicleRepository(Tracking_ShipmentContext context)
        {
            _context = context;
        }

        public async Task<string> AddVehicleAsync(VehicleDTO vehicleDto)
        {
            var vehicle = new Vehicle
            {
                VehicleId = Guid.NewGuid().ToString().Substring(0, 9),
                VehicleType = vehicleDto.VehicleType ?? "",
                Capacity = vehicleDto.Capacity,
                Price = vehicleDto.price,
                LicensePlate = vehicleDto.LicensePlate ?? "",
            };

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle.VehicleId;
        }

        public async Task<List<VehicleDTO>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles
                .Select(v => new VehicleDTO
                {
                    VehicleId = v.VehicleId,
                    VehicleType = v.VehicleType,
                    Capacity = v.Capacity ?? 0,
                    price = v.Price ?? 0.0
                }).ToListAsync();
        }

        public async Task<VehicleDTO?> GetVehicleByIdAsync(string vehicleId)
        {
            var v = await _context.Vehicles.FindAsync(vehicleId);
            if (v == null) return null;

            return new VehicleDTO
            {
                VehicleId = v.VehicleId,
                VehicleType = v.VehicleType,
                Capacity = v.Capacity ?? 0,
                price = v.Price ?? 0.0
            };
        }

        public async Task<bool> UpdateVehicleAsync(string vehicleId, VehicleDTO vehicleDto)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null) return false;

            vehicle.VehicleType = vehicleDto.VehicleType ?? "";
            vehicle.Capacity = vehicleDto.Capacity;
            vehicle.Price = vehicleDto.price;
            // Không cập nhật Status hoặc LicensePlate

            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehicleAsync(string vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle == null) return false;

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
