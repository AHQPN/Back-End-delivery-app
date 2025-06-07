using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Location
    {
        public Location()
        {
            OrderDestinationLocationNavigations = new HashSet<Order>();
            OrderSourceLocationNavigations = new HashSet<Order>();
        }

        public int LocationId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Order> OrderDestinationLocationNavigations { get; set; }
        public virtual ICollection<Order> OrderSourceLocationNavigations { get; set; }
    }
}
