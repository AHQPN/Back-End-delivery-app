namespace Backend_Mobile_App.Controllers
{
    public class LocationDTO
    {
        public LocationDTO(decimal? latitude, decimal? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}