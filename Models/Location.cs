using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual Order? OrderDestinationLocationNavigation { get; set; }
        public virtual Order? OrderSourceLocationNavigation { get; set; }
        public virtual User? User { get; set; }
    }
}
