namespace Backend_Mobile_App.DTOs
{
    public class UserDTO
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string Role { get; set; } = null!;
        
    }
}
