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

        public async Task<Location?> SaveLocationByLatLongAsync(decimal latitude, decimal longitude)
        {    
            var location = new Location 
                {
                    Latitude = latitude,
                    Longitude = longitude
                };
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }
    }
}
