using Naobiz.Data;

namespace Naobiz.Models.Emails
{
    public class ForumMessageModel : BaseModel
    {
        public ForumMessageModel(ForumMessage message)
        {
            Message = message;
        }

        public ForumMessage Message { get; }
    }
}
