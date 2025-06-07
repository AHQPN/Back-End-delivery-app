namespace Backend_Mobile_App.DTOs
{
    public class LoginResultDTO
    {
        public string Token { get; set; } = null!;
        public UserDTO User { get; set; } = null!;
    }
}
