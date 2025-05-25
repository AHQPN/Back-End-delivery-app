namespace Backend_Mobile_App.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "User"; // mặc định
    }
}
