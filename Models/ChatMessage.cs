using System;
using System.Collections.Generic;

namespace Backend_Mobile_App.Models
{
    public partial class ChatMessage
    {
        public string MessageId { get; set; } = null!;
        public string SenderId { get; set; } = null!;
        public string ReceiverId { get; set; } = null!;
        public string MessageContent { get; set; } = null!;
        public DateTime? SentAt { get; set; }
        public bool? IsRead { get; set; }

        public virtual User Receiver { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}
