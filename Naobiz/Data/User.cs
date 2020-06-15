using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Naobiz.Data
{
    public class User
    {
        public int Id { get; set; }

        public bool Admin { get; set; }

        public bool Paid { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public DateTime? LoginDateTime { get; set; }

        [MaxLength(15)]
        public string LoginIpAddress { get; set; }

        [MaxLength(32)]
        public string ActivationCode { get; set; }

        [MaxLength(32)]
        public string PasswordResetCode { get; set; }

        [Required]
        [MaxLength(254)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [MaxLength(64)]
        public string PasswordHash { get; set; }

        public void SetPassword(string password) => PasswordHash = GetPasswordHash(password);

        public bool VerifyPassword(string password) => PasswordHash == GetPasswordHash(password);

        private static string GetPasswordHash(string password)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return string.Join(null, hash.Select(b => b.ToString("x2")));
        }

        [MaxLength(9)]
        public string TaxId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Company { get; set; }

        public string NameWithCompany
        {
            get
            {
                var name = Name;
                if (Company != null)
                {
                    name += $" - {Company}";
                }
                return name;
            }
        }

        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(2)]
        public string Country { get; set; }

        [MaxLength(6)]
        public string Province { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(5)]
        public string ZipCode { get; set; }

        public bool NotifyForum { get; set; }

        public bool NotifyChat { get; set; }

        public bool InfoRequested { get; set; }

        public virtual UserGroup Group { get; set; }

        public void Initialize(bool activated)
        {
            RegistrationDateTime = DateTime.Now;
            if (!activated)
            {
                ActivationCode = Guid.NewGuid().ToString("N");
            }
            Paid = true;
        }

        public void ResetPassword()
        {
            PasswordResetCode = Guid.NewGuid().ToString("N");
        }

        public void UpdateLoginInfo(IPAddress ipAddress)
        {
            LoginDateTime = DateTime.Now;
            LoginIpAddress = ipAddress.ToString();
        }
    }
}
