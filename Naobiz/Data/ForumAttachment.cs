namespace Naobiz.Data
{
    public class ForumAttachment : Attachment
    {
        public int MessageId { get; set; }

        public virtual ForumMessage Message { get; set; }
    }
}
