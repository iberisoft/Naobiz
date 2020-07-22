using System;
using System.ComponentModel.DataAnnotations;

namespace Naobiz.Data
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public virtual User User { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public decimal Value { get; set; }

        public decimal ValueWithoutTax(decimal taxRate) => Value / (1 + taxRate);

        public decimal TaxValue(decimal taxRate) => Value - ValueWithoutTax(taxRate);

        [MaxLength(20)]
        public string PaypalToken { get; set; }

        public bool Completed { get; set; }

        public string InvoicelId => $"{CreationDateTime:yy}-{Id:d4}";

        public void Initialize(User user)
        {
            User = user;
            CreationDateTime = DateTime.Now;
            StartDateTime = CreationDateTime;
            var maxOrderedDateTime = user.MaxOrderedDateTime();
            if (StartDateTime < maxOrderedDateTime)
            {
                StartDateTime = maxOrderedDateTime.Value;
            }
            EndDateTime = StartDateTime.AddYears(1);
        }
    }
}
