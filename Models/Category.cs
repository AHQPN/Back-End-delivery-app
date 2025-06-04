using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Category
    {
        public Category()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
