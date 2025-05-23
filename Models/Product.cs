using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public string ProductId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? SizeId { get; set; }
        public int? Weight { get; set; }
        public string? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Size? Size { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
