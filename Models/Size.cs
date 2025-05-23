using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Size
    {
        public Size()
        {
            Products = new HashSet<Product>();
        }

        public string SizeId { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
