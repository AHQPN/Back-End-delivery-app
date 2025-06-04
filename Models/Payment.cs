﻿using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Payment
    {
        public string PaymentId { get; set; } = null!;
        public string OrderId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
