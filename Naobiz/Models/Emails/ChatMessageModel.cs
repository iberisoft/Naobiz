using Naobiz.Data;

namespace Naobiz.Models.Emails
{
    public class ChatMessageModel : BaseModel
    {
        public ChatMessageModel(ChatMessage message)
        {
            Message = message;
        }

        public ChatMessage Message { get; }
    }
}
