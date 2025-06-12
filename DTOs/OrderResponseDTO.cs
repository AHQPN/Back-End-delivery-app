using Backend_Mobile_App.Controllers;

namespace Backend_Mobile_App.DTOs
{
    public class OrderResponseDTO
    {
        public string? OrderID { get; set; }
        public LocationDTO? SourceLocation { get; set; }
        public LocationDTO? DestinationLocation { get; set; }
        public string? VehicleId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CustomerId { get; set; }
        public string? TenNguoiNhan { get; set; }
    }
}
