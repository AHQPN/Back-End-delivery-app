using Backend_Mobile_App.Controllers;

namespace Backend_Mobile_App.DTOs
{
    public class OrderResponseDTO
    {
        //public OrderResponseDTO(string orderID, LocationDTO sourceLocation, LocationDTO destinationLocation, string vehicleType, decimal totalAmount, string orderStatus, string paymentStatus, DateTime? createdAt)
        //{
        //    OrderID = orderID;
        //    SourceLocation = sourceLocation;
        //    DestinationLocation = destinationLocation;
        //    VehicleType = vehicleType;
        //    TotalAmount = totalAmount;
        //    OrderStatus = orderStatus;
        //    PaymentStatus = paymentStatus;
        //    CreatedAt = createdAt;
        //}

        public string? OrderID { get; set; }
        public LocationDTO? SourceLocation { get; set; }
        public LocationDTO? DestinationLocation { get; set; }
        public string? VehicleType { get; set; }
        public decimal TotalAmount { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public VehicleDTO? Vehicle { get; set; }

    }
}
