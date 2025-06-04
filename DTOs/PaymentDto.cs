namespace Backend_Mobile_App.Controllers
{
    public class PaymentDto
    {
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TransactionID { get; set; }
        public double PaymentAmount { get; set; }
    }
}