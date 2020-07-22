using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class UserGroup
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }

        public bool Paid { get; set; }
    }
}
