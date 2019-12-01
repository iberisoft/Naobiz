using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class ForumAttachment
    {
        public int Id { get; set; }

        [Required]
        public virtual ForumMessage Message { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public long Size { get; set; }

        [Required]
        public virtual Blob Content { get; set; }
    }
}
