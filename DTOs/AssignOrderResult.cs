namespace Backend_Mobile_App.DTOs
{
    public class AssignOrderResultDto
    {
        public string OrderId { get; set; }
        public string DeliveryPersonId { get; set; }
        public double Distance { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}