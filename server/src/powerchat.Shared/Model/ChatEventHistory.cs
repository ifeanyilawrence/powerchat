namespace powerchat.Shared.Model
{
    using System;
    using Enum;
    
    public class ChatEventHistory
    {
        public Guid Id { get; set; }

        public DateTime Time { get; set; }

        public UserDto User { get; set; }

        public ChatEventType EventTypeId { get; set; }

        public string Comment { get; set; }

        public UserDto EventForUser { get; set; }
    }
}