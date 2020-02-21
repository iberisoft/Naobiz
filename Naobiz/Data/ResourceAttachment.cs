namespace Naobiz.Data
{
    public class ResourceAttachment : Attachment
    {
        public int ResourceId { get; set; }

        public virtual Resource Resource { get; set; }
    }
}
