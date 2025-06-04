using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Service
    {
        public Service()
        {
            Orders = new HashSet<Order>();
        }

        public string ServiceId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
