namespace Backend_Mobile_App.DTOs
{
    public class UserDto
    {
        public string UserId { get; set; } = null!; 
        public string UserName { get; set; } = null!; 
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; } 
        
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}