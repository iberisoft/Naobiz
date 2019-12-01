using System;
using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class ForumMessage
    {
        public int Id { get; set; }

        [Required]
        public virtual User Creator { get; set; }

        public DateTime CreationDateTime { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        public virtual ForumMessage ParentMessage { get; set; }

        public void Initialize(User creator)
        {
            Creator = creator;
            CreationDateTime = DateTime.Now;
        }
    }
}
