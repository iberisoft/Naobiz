using Naobiz.Data;

namespace Naobiz.Models
{
    class AccountModel
    {
        public AccountModel()
        {
            Country = DefaultCountry;
        }

        public const string DefaultCountry = "ES";

        public string TaxId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        string m_Country;

        public string Country
        {
            get => m_Country;
            set
            {
                if (m_Country != value)
                {
                    m_Country = value;
                    if (m_Country != DefaultCountry)
                    {
                        Province = null;
                    }
                }
            }
        }

        public string Province { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public bool InfoRequested { get; set; }

        public void CopyTo(User user)
        {
            user.TaxId = TaxId;
            user.Name = Name;
            user.Phone = Phone;
            user.Address = Address;
            user.Country = Country;
            user.Province = Province;
            user.City = City;
            user.ZipCode = ZipCode;
            user.InfoRequested = InfoRequested;
        }

        public void CopyFrom(User user)
        {
            TaxId = user.TaxId;
            Name = user.Name;
            Phone = user.Phone;
            Address = user.Address;
            Country = user.Country;
            Province = user.Province;
            City = user.City;
            ZipCode = user.ZipCode;
            InfoRequested = user.InfoRequested;
        }
    }
}
