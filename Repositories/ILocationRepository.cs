using Backend_Mobile_App.Controllers;
using Backend_Mobile_App.DTOs;
using Backend_Mobile_App.Models;
using Backend_Mobile_App.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_Mobile_App.Repositories
{
    public interface ILocationRepository
    {
        Task<Location?> SaveLocationByLatLongAsync(decimal latitude, decimal longitude);
    }
}
