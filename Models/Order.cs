using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Order
    {
        public Order()
        {
            Deliveries = new HashSet<Delivery>();
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
        }

        public string OrderId { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string OrderStatus { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
