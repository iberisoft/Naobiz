using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class Blob
    {
        public int Id { get; set; }

        [Required]
        public byte[] Data { get; set; }
    }
}
