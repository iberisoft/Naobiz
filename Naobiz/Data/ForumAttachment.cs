using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class ForumAttachment : Attachment
    {
        [Required]
        public virtual ForumMessage Message { get; set; }
    }
}
