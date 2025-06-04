using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public string OrderId { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Serviceid { get; set; }
        public string? DeliveryPersonId { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }
        public DateTime? ActualDeliveryTime { get; set; }
        public int? DestinationLocation { get; set; }
        public int? SourceLocation { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual Location? DestinationLocationNavigation { get; set; }
        public virtual Service? Service { get; set; }
        public virtual Location? SourceLocationNavigation { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
