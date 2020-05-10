using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class ForumGroup
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(800)]
        public string ImageSource { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
