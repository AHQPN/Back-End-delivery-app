using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Assignment
    {
        public string AssignmentId { get; set; } = null!;
        public string? DeliveryPersonId { get; set; }
        public string? VehicleId { get; set; }
        public DateTime? AssignedDate { get; set; }

        public virtual User? DeliveryPerson { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}
