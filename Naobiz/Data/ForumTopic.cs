using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class ForumTopic
    {
        public int Id { get; set; }

        [Required]
        public virtual ForumGroup Group { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public virtual ForumMessage Message { get; set; }
    }
}
