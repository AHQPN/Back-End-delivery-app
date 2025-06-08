using Backend_Mobile_App.Models;

namespace Backend_Mobile_App.Controllers
{
    public class OrderCreateDto
    {
        public string CustomerId { get; set; } = null!;
        public string? Serviceid { get; set; }
        public string? DeliveryPersonId { get; set; }
        public decimal TotalAmount { get; set; }
        public LocationDTO? SourceLocation { get; set; }
        public LocationDTO? DestinationLocation { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }
        public DateTime? ActualDeliveryTime { get; set; }
        public DateTime? PickupTime { get; set; }
        public string? TenNguoiNhan { get; set; }
        public string? SdtnguoiNhan { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public PaymentDto? Payment { get; set; }
    }
}