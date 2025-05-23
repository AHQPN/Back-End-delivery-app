using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class Notification
    {
        public string NotificationId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? Title { get; set; }
        public string Message { get; set; } = null!;
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
