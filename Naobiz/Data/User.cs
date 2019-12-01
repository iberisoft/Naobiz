using System;
using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class User
    {
        public int Id { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        [Required]
        [MaxLength(15)]
        public string RegistrationIpAddress { get; set; }

        [MaxLength(32)]
        public string ActivationCode { get; set; }

        [MaxLength(32)]
        public string PasswordResetCode { get; set; }

        [Required]
        [MaxLength(254)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [MaxLength(9)]
        public string TaxId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(2)]
        public string Country { get; set; }

        [MaxLength(6)]
        public string Province { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(5)]
        public string ZipCode { get; set; }

        public bool InfoRequested { get; set; }

        public void Initialize(string ipAddress, bool activated)
        {
            RegistrationDateTime = DateTime.Now;
            RegistrationIpAddress = ipAddress;
            if (!activated)
            {
                ActivationCode = Guid.NewGuid().ToString("N");
            }
        }

        public void ResetPassword()
        {
            PasswordResetCode = Guid.NewGuid().ToString("N");
        }
    }
}
