namespace Backend_Mobile_App.Controllers
{
    public class OrderCreateDto
    {
        public string? CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; }
        public PaymentDto? Payment { get; set; }
    }
}