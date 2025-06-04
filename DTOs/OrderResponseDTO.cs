namespace Backend_Mobile_App.DTOs
{
    public class OrderResponseDTO
    {
        public int Id { get;}
        public string? OrderStatus {  get;}
        public double TotalAmount { get;}
        public DateTime? CreatedDate { get;}
        public List<DeliveryResponseDTO> Deliveries { get; }
        public string? VehicleName { get;}

        public OrderResponseDTO(int id, string? orderStatus, double totalAmount, DateTime? createdDate, string? vehicleName)
        {
            Id = id;
            OrderStatus = orderStatus;
            TotalAmount = totalAmount;
            CreatedDate = createdDate;
            VehicleName = vehicleName;
        }
    }
}
