using Naobiz.Data;

namespace Naobiz.Models.Emails
{
    public class BaseModel
    {
        public User User { get; set; }

        public string SupportUrl { get; set; }

        public string SiteUrl { get; set; }

        public string SiteEmail { get; set; }
    }
}
