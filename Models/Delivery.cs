using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Delivery
    {
        public string DeliveryId { get; set; } = null!;
        public string OrderId { get; set; } = null!;
        public string? DeliveryPersonId { get; set; }
        public string DeliveryStatus { get; set; } = null!;
        public DateTime? EstimatedDeliveryTime { get; set; }
        public DateTime? ActualDeliveryTime { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? DeliveryPerson { get; set; }
        public virtual Order Order { get; set; } = null!;
    }
}
