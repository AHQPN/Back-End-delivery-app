using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Vehicle
    {
        public string VehicleId { get; set; } = null!;
        public string VehicleType { get; set; } = null!;
        public string LicensePlate { get; set; } = null!;
        public int? Capacity { get; set; }
        public string? Status { get; set; }
        public double? Price { get; set; }

        public virtual Assignment? Assignment { get; set; }
    }
}
