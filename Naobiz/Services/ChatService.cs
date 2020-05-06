using Naobiz.Data;
using System;

namespace Naobiz.Services
{
    class ChatService
    {
        public bool SendMessage(ChatMessage message)
        {
            var e = new MessageEventArgs(message);
            MessageSent?.Invoke(this, e);
            return e.Delivered;
        }

        public event EventHandler<MessageEventArgs> MessageSent;

        public class MessageEventArgs
        {
            public MessageEventArgs(ChatMessage message)
            {
                CreatorId = message.Creator.Id;
                RecipientId = message.Recipient.Id;
            }

            public int CreatorId { get; }

            public int RecipientId { get; }

            public bool Delivered { get; set; }
        }
    }
}
