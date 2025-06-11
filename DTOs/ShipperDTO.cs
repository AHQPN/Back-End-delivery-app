namespace Backend_Mobile_App.DTOs
{
    public class ShipperDTO
    {
      

        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string  Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        

        public string? VehicleType { get; set; }
        public string? VehiclePlate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
