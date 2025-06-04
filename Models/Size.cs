using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Size
    {
        public Size()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public string SizeId { get; set; } = null!;
        public string? Description { get; set; }
        public double? Weight { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
