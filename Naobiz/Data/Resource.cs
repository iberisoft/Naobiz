using System;
using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class Resource
    {
        public int Id { get; set; }

        public DateTime CreationDateTime { get; set; }

        [Required]
        public virtual User Owner { get; set; }

        [Required]
        public virtual ResourceType Type { get; set; }

        [Required]
        public virtual Activity Activity { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(800)]
        public string Description { get; set; }

        [MaxLength(800)]
        public string Url { get; set; }

        public void Initialize()
        {
            CreationDateTime = DateTime.Now;
        }
    }
}
