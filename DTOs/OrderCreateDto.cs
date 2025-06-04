using Backend_Mobile_App.Models;

namespace Backend_Mobile_App.Controllers
{
    public class OrderCreateDto
    {
        public string? CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
        public string? serviceId { get; set; }
        public LocationDTO? SourceLocation { get; set; }
        public LocationDTO? DestinationLocation { get; set; }
        public DateTime? PickupTime { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; }
        public PaymentDto? Payment { get; set; }
    }
}