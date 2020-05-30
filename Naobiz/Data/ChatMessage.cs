using System;
using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class ChatMessage
    {
        public int Id { get; set; }

        [Required]
        public virtual User Creator { get; set; }

        public DateTime CreationDateTime { get; set; }

        [Required]
        public virtual User Recipient { get; set; }

        [Required]
        [MaxLength(250)]
        public string Text { get; set; }

        public void Initialize(User creator, User recipient)
        {
            Creator = creator;
            CreationDateTime = DateTime.Now;
            Recipient = recipient;
        }
    }
}
