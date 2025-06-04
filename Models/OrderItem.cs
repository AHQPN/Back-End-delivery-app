using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class OrderItem
    {
        public string OrderItemId { get; set; } = null!;
        public string? OrderId { get; set; }
        public string? SizeId { get; set; }
        public string? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Size? Size { get; set; }
    }
}
