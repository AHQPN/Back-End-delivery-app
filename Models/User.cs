using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class User
    {
        public User()
        {
            ChatMessageReceivers = new HashSet<ChatMessage>();
            ChatMessageSenders = new HashSet<ChatMessage>();
            Deliveries = new HashSet<Delivery>();
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
        }

        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; } = null!;
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual Assignment? Assignment { get; set; }
        public virtual ICollection<ChatMessage> ChatMessageReceivers { get; set; }
        public virtual ICollection<ChatMessage> ChatMessageSenders { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
