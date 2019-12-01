using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
