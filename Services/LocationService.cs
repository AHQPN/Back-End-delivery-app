using AutoMapper;
using Backend_Mobile_App.Controllers;
using Backend_Mobile_App.Data;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend_Mobile_App.Services
{
    public class LocationService : ILocationRepository 
    {
        public readonly Tracking_ShipmentContext _context;
        public readonly IMapper _mapper;

        public LocationService(Tracking_ShipmentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }        

        public async Task<Location?> SaveLocationByLatLongAsync(LocationDTO locationDTO)
        {
            var newLocation = _mapper.Map<Location>(locationDTO);

            _context.Locations.Add(newLocation);
            await _context.SaveChangesAsync();
            return newLocation;
        }
    }
}
