using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class ResourceAttachment : Attachment
    {
        [Required]
        public virtual Resource Resource { get; set; }
    }
}
